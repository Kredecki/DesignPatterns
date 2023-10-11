using Mediator.Enums;
using Mediator.Lib;
using Mediator.Models;
using Mediator.Queries;
using MediatR;

namespace Mediator.Handlers
{
    public class GetAllPostsQueryHandler : IRequestHandler
    <GetAllPostsQuery, List<Post>>
    {
        public Task<List<Post>> Handle(GetAllPostsQuery request,
            CancellationToken cancellationToken)
        {
            var posts = DummyPosts.Get();
            if (request.OrderBy == OrderByPostOptions.ByAuthor)
                return Task.FromResult
                    (posts.OrderBy(p => p.Author).ToList());
            else if (request.OrderBy == OrderByPostOptions.ByDate)
                return Task.FromResult
                    (posts.OrderBy(p => p.Date).ToList());
            else if (request.OrderBy == OrderByPostOptions.ByTitle)
                return Task.FromResult
                    (posts.OrderBy(p => p.Title).ToList());

            return Task.FromResult
                   (posts);
        }
    }
}
