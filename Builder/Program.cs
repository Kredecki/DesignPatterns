using System;
using System.Text;

namespace Builder
{
    #region HtmlBuilder
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int IndentSize = 2;

        public HtmlElement()
        {

        }

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            StringBuilder sb = new StringBuilder();
            string i = new string(' ', IndentSize * indent);
            sb.Append($"{i}<{Name}>");
            sb.Append("\n");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', IndentSize * (indent+1)));
                sb.AppendLine(Text);
            }

            foreach(var e in Elements)
                sb.Append(e.ToStringImpl(indent + 1));
            
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public HtmlBuilder AddChildFluent(string childName, string childText)
        {
            HtmlElement e = new(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }
    #endregion FluentBuilder End

    #region StepwiseBuilder
    public enum CarType
    {
        Sedan,
        Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }

    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        public iBuildCar WithWheels(int size);
    }

    public interface iBuildCar 
    {
        public Car Build();
    }

    public class CarBuilder
    {
        private class Impl :
            ISpecifyCarType,
            ISpecifyWheelSize,
            iBuildCar
        {
            private Car car = new Car();

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public iBuildCar WithWheels(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
                }
                car.WheelSize = size;
                return this;
            }

            public Car Build()
            {
                return car;
            }
        }

        public static ISpecifyCarType Create()
        {
            return new Impl();
        }
    }
    #endregion StepwiseBuilder End

    #region FluentBuilder
    public class Person
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public Person() { }

        public Person(string name, string position)
        {
            Name = name;
            Position = position;
        }
    }

    public sealed class PersonBuilder
    {
        public readonly List<Action<Person>> Actions
            = new();

        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAs
            (this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }
    #endregion FluentBuilder end

    #region FacetedBuilder
    public class Personf
    {
        //Address
        public string StreedAddress, Postcode, City;

        //employment
        public string CompanyName, position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreedAddress)}: {StreedAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(position)}: {position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilderf
    {
        protected Personf person = new();

        public PersonJobBuilderf Works => new(person);
        public PersonAddressBuilder Lives => new(person);

        public static implicit operator Personf(PersonBuilderf pb)
        {
            return pb.person;
        }
    }

    public class PersonJobBuilderf : PersonBuilderf
    {
        public PersonJobBuilderf(Personf person)
        {
            this.person = person;
        }

        public PersonJobBuilderf At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilderf AsA(string position)
        {
            person.position = position;
            return this;
        }

        public PersonBuilderf Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilderf
    {
        public PersonAddressBuilder(Personf person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreedAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }
    }

    #endregion FacetedBuilder end

    public class Program
    {
        static void Main(string[] args)
        {
            #region HtmlBuilderImplementation
            HtmlBuilder builder = new("ul");
            builder.AddChildFluent("li", "Hello").AddChildFluent("li", "World");
            Console.WriteLine(builder);
            #endregion HtmlBuilderImplementation

            #region StepwiseBuilderImplementation
            var car = CarBuilder.Create()
                .OfType(CarType.Crossover)
                .WithWheels(18)
                .Build();
            #endregion StepwiseBuilderImplementation End

            #region FunctionalBuildImplementation
            var person = new PersonBuilder()
                .Called("Sarah")
                .WorksAs("Developer")
                .Build();

            Console.WriteLine(person.Name);
            #endregion FunctionalBuildImplementation end

            #region FacetedBuilderImplementation
            var pb = new PersonBuilderf();
            Personf personf = pb
                .Lives.At("Adama Mickiewicza")
                    .In("Krosno")
                    .WithPostcode("38-400")
                .Works.At("Fabrikam")
                    .AsA("Engineer")
                    .Earning(3000);

            Console.WriteLine(personf);
            #endregion FacetedBuilderImplementation end
        }
    }
}