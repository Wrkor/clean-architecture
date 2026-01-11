namespace Api.Controllers;

[AllowAnonymous]
[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> LoginAsync(
        LoginRequestDto @object,
        CancellationToken ct
    )
    {
        var query = new LoginUserQuery(@object);
        var result = await mediator.Send(query, ct);
        return Results.Ok(result);
    }

    [HttpPost("register")]
    public async Task<IResult> RegisterAsync(
        RegisterUserRequestDto @object,
        CancellationToken ct
    )
    {
        var command = new RegisterUserCommand(@object);
        var result = await mediator.Send(command, ct);
        return Results.Ok(result);
    }
}
