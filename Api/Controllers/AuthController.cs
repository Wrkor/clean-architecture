using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[AllowAnonymous]
[Route("api/auth")]
[ApiController]
public class AuthController(
    UserManager<CustomIdentityUser> manager,
    IJwtService jwtService
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> LoginAsync(LoginRequestDto @object)
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

        var token = jwtService.CreateToken(user);
        var result = new IdentityUserResponseDto(
            user.UserName,
            user.Email,
            token
        );

        return Results.Ok(new { @object = result });
    }
}
