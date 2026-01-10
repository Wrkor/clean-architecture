namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class TopicsController(ITopicService topicService) : ControllerBase
    {
        [HttpGet("topics")]
        public async Task<ActionResult<List<Topic>>> GetTopicsAsync(CancellationToken ct)
        {
            var result = await topicService.GetTopicsAsync(ct);

            return Ok(result);
        }
    }
}
