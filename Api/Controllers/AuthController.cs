namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(UserManager<CustomIdentityUser> manager)
        : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequestDto @object)
        {
            var user = await manager.FindByEmailAsync(@object.Email);
            if (user is null)
                return Results.Unauthorized();

            var isVerify = await manager.CheckPasswordAsync(
                user,
                @object.Password
            );

            if (!isVerify)
                return Results.Unauthorized();

            var result = new IdentityUserResponseDto(
                user.UserName!,
                user.Email!,
                "jwt"
            );
            return Results.Ok(new { @object = result });
        }
    }
}
