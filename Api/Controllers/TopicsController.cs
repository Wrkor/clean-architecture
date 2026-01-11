using Application.Topics.Queries.GetTopics;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class TopicsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("topics")]
        public async Task<ActionResult<List<TopicResponseDto>>> GetTopicsAsync(
            CancellationToken ct
        )
        {
            var query = new GetTopicsQuery();
            var result = await mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet("topics/{id}")]
        public async Task<ActionResult<string>> GetTopicAsync(
            Guid id,
            CancellationToken ct
        )
        {
            return Ok();
        }

        [HttpPost("topics")]
        public async Task<ActionResult<TopicResponseDto>> CreateTopicAsync(
            CreateTopicDto @object,
            CancellationToken ct
        )
        {
            return Created();
        }

        [HttpPut("topics/{id}")]
        public async Task<ActionResult<TopicResponseDto>> UpdateTopicAsync(
            Guid id,
            UpdateTopicDto @object,
            CancellationToken ct
        )
        {
            return Ok();
        }

        [HttpDelete("topics/{id}")]
        public async Task<ActionResult> DeleteAsync(
            Guid id,
            CancellationToken ct
        )
        {
            return NoContent();
        }
    }
}
