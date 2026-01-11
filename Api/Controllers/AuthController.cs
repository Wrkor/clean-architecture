namespace Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    UserManager<CustomIdentityUser> manager,
    IJwtService jwtService
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto @object)
    {
        var user = await manager.FindByEmailAsync(@object.Email);
        if (user is null)
            return Results.Unauthorized();

        var isVerify = await manager.CheckPasswordAsync(user, @object.Password);

        if (!isVerify)
            return Results.Unauthorized();

        var token = jwtService.CreateToken(user);
        var result = new IdentityUserResponseDto(
            user.UserName!,
            user.Email!,
            token
        );
        return Results.Ok(new { @object = result });
    }
}
