using Mediator.Models;

namespace Mediator.Lib
{
    public static class DummyPosts
    {
        public static List<Post> Get()
        {
            Post p1 = new Post()
            {
                Author = "Damian",
                Date = DateTime.Now.AddMonths(-6),
                Description = @"Post1.",
                ImageUrl = "https://cezarywalenciuk.pl/Posts/programing/icons/_withbackground/R2/656_walidacja-z-fluentvalidation-waspnet-core--swagger.png",
                PostId = 1,
                CategoryId = DummySeed.Csharp,
                Rate = 8,
                Title = "Walidacja z FluentValidation w ASP.NET Core + Swagger",
                Url = "https://cezarywalenciuk.pl/blog/programing/walidacja-z-fluentvalidation-waspnet-core--swagger"
            };

            Post p2 = new Post()
            {
                Author = "Damian",
                Date = DateTime.Now.AddMonths(-6),
                Description = @"Post2.",
                ImageUrl = "https://cezarywalenciuk.pl/Posts/programing/icons/_withbackground/R2/656_walidacja-z-fluentvalidation-waspnet-core--swagger.png",
                PostId = 2,
                CategoryId = DummySeed.Aspnet,
                Rate = 7,
                Title = "Swagger UI : Dokumentowanie API w ASP.NET CORE",
                Url = "https://cezarywalenciuk.pl/blog/programing/swagger-ui--dokumentowanie-api-w-aspnet-core"
            };

            List<Post> p = new List<Post>();
            p.Add(p1);
            p.Add(p2);

            return p;
        }
     }
}
