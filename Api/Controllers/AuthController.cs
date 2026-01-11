namespace Api.Controllers;

[AllowAnonymous]
[Route("api/auth")]
[ApiController]
public class AuthController(
    UserManager<CustomIdentityUser> manager,
    IJwtService jwtService,
    IMediator mediator
) : ControllerBase
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
    public async Task<IResult> RegisterAsync(RegisterUserRequestDto @object)
    {
        var isUserExists = await manager.Users.AnyAsync(u =>
            u.UserName == @object.UserName
        );

        if (isUserExists)
            return Results.BadRequest("Username занят");

        isUserExists = await manager.Users.AnyAsync(u =>
            u.Email == @object.Email
        );

        if (isUserExists)
            return Results.BadRequest("Email занят");

        var user = new CustomIdentityUser()
        {
            FullName = @object.FullName,
            Email = @object.Email,
            UserName = @object.UserName,
            About = string.Empty,
        };

        var identityResult = await manager.CreateAsync(user, @object.Password);

        if (!identityResult.Succeeded)
            return Results.BadRequest(identityResult.Errors);

        var token = jwtService.CreateToken(user.Id, user.UserName, user.Email);

        var result = new IdentityUserResponseDto(
            user.UserName,
            user.Email,
            token
        );

        return Results.Ok(new { @object = result });
    }
}
