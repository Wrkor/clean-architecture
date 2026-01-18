namespace Api.Controllers;

[Route("api")]
[ApiController]
public class CommentsController(IMediator mediator) : ControllerBase
{
    [HttpGet("comments/{id}")]
    [ProducesResponseType(type: typeof(GetCommentsResult), 200)]
    public async Task<IResult> GetCommentsAsync(Guid id, CancellationToken ct)
    {
        var query = new GetCommentsQuery(id);
        var result = await mediator.Send(query, ct);
        return Results.Ok(result);
    }

    [HttpPost("comments")]
    [ProducesResponseType(type: typeof(CreateCommentResult), 200)]
    public async Task<IResult> CreateCommentAsync(
        CreateCommentDto @object,
        CancellationToken ct
    )
    {
        var command = new CreateCommentCommand(@object);
        var result = await mediator.Send(command, ct);
        var path = $"api/Comments/{result.Object.Id}";
        return Results.Created(path, result);
    }
}
