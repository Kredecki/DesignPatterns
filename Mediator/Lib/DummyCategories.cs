using Mediator.Models;

namespace Mediator.Lib
{
    public static class DummyCategories
    {
        public static List<Category> Get()
        {
            Category c1 = new Category()
            {
                CategoryId = DummySeed.Csharp,
                Name = "CSharp",
                DisplayName = "C#"
            };

            Category c2 = new Category()
            {
                CategoryId = DummySeed.Aspnet,
                Name = "aspnet",
                DisplayName = "ASP.NET"
            };

            List<Category> p = new List<Category>();
            p.Add(c1);
            p.Add(c2);

            return p;
        }
    }
}
