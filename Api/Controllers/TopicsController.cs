namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class TopicsController(ITopicService topicService) : ControllerBase
    {
        [HttpGet("topics")]
        public async Task<ActionResult<List<TopicResponseDto>>> GetTopicsAsync(CancellationToken ct)
        {
            var result = await topicService.GetTopicsAsync(ct);
            return Ok(result);
        }

        [HttpGet("topics/{id}")]
        public async Task<ActionResult<string>> GetTopicAsync(Guid id, CancellationToken ct)
        {
            var result = await topicService.GetTopicAsync(id, ct);
            return Ok(result);
        }

        [HttpPost("topics")]
        public async Task<ActionResult<TopicResponseDto>> CreateTopicAsync(
            CreateTopicDto @object,
            CancellationToken ct
        )
        {
            var result = await topicService.CreateTopicAsync(@object, ct);
            return Ok(result);
        }
    }
}
