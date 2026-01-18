using Application.Topics.Commands.JoinLeaveTopic;

namespace Api.Controllers;

[Route("api")]
[ApiController]
public class TopicsController(IMediator mediator) : ControllerBase
{
    [HttpGet("topics")]
    [ProducesResponseType(type: typeof(GetTopicsResult), 200)]
    public async Task<IResult> GetTopicsAsync(CancellationToken ct)
    {
        var query = new GetTopicsQuery();
        var result = await mediator.Send(query, ct);
        return Results.Ok(result);
    }

    [HttpGet("topics/{id}")]
    [ProducesResponseType(type: typeof(GetTopicByIdResult), 200)]
    public async Task<IResult> GetTopicAsync(Guid id, CancellationToken ct)
    {
        var query = new GetTopicByIdQuery(id);
        var result = await mediator.Send(query, ct);
        return Results.Ok(result);
    }

    [HttpPost("topics")]
    [ProducesResponseType(type: typeof(CreateTopicResult), 200)]
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
    [ProducesResponseType(type: typeof(UpdateTopicResult), 200)]
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
    [Authorize(Policy = "IsTopicAuthor")]
    [ProducesResponseType(type: typeof(DeleteTopicResult), 200)]
    public async Task<IResult> DeleteAsync(Guid id, CancellationToken ct)
    {
        var command = new DeleteTopicCommand(id);
        await mediator.Send(command, ct);
        return Results.NoContent();
    }

    [HttpPost("topics/{id}/join")]
    [ProducesResponseType(type: typeof(DeleteTopicResult), 200)]
    public async Task<IResult> JoinLeaveTopic(Guid id, CancellationToken ct)
    {
        var command = new JoinLeaveTopicCommand(id);
        var result = await mediator.Send(command, ct);
        return Results.Ok(result);
    }
}
