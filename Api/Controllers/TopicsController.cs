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
            var command = new CreateTopicCommand(@object);
            var result = await mediator.Send(command, ct);
            var path = $"api/topics/{result.Object.Id}";
            return Results.Created(path, result);
        }

        [HttpPut("topics/{id}")]
        public async Task<IResult> UpdateTopicAsync(
            Guid id,
            UpdateTopicDto @object,
            CancellationToken ct
        )
        {
            var command = new UpdateTopicCommand(id, @object);
            var result = await mediator.Send(command, ct);
            return Results.Ok(result);
        }

        [HttpDelete("topics/{id}")]
        public async Task<IResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var command = new DeleteTopicCommand(id);
            await mediator.Send(command, ct);
            return Results.NoContent();
        }
    }
}
