using Mediator.Enums;
using Mediator.Models;
using MediatR;

namespace Mediator.Queries
{
    public class GetAllPostsQuery
    : IRequest<List<Post>>
    {
        public OrderByPostOptions OrderBy { get; set; }
    }
}
