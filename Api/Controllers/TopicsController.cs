using Application.Topics.Queries.GetTopicById;
using Application.Topics.Queries.GetTopics;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class TopicsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("topics")]
        public async Task<IResult> GetTopicsAsync(CancellationToken ct)
        {
            var query = new GetTopicsQuery();
            var result = await mediator.Send(query, ct);
            return Results.Ok(result);
        }

        [HttpGet("topics/{id}")]
        public async Task<IResult> GetTopicAsync(Guid id, CancellationToken ct)
        {
            var query = new GetTopicByIdQuery(id);
            var result = await mediator.Send(query, ct);
            return Results.Ok(result);
        }

        [HttpPost("topics")]
        public async Task<IResult> CreateTopicAsync(
            CreateTopicDto @object,
            CancellationToken ct
        )
        {
            return Results.Created();
        }

        [HttpPut("topics/{id}")]
        public async Task<IResult> UpdateTopicAsync(
            Guid id,
            UpdateTopicDto @object,
            CancellationToken ct
        )
        {
            return Results.Ok();
        }

        [HttpDelete("topics/{id}")]
        public async Task<IResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            return Results.NoContent();
        }
    }
}
