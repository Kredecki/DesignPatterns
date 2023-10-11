using Mediator.Commands;
using Mediator.Models;
using Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mediator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllPosts")]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            var request = new GetAllPostsQuery
            {
                OrderBy = Enums.OrderByPostOptions.ByDate
            };
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpPut(Name = "UpdatePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody]
        UpdatePostCommand updatePostCommand)
        {
            await _mediator.Send(updatePostCommand);

            return NoContent();
        }
    }
}
