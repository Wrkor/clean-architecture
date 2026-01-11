namespace Application.Security.Queries.LoginUser;

public class LoginUserHandler(
    UserManager<CustomIdentityUser> manager,
    IJwtService jwtService
) : IQueryHandler<LoginUserQuery, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(
        LoginUserQuery request,
        CancellationToken ct
    )
    {
        var user = await manager.FindByEmailAsync(request.Object.Email);

        if (user is null)
            throw new UserEmailNotFoundException(request.Object.Email);

        var isVerify = await manager.CheckPasswordAsync(
            user,
            request.Object.Password
        );

        if (!isVerify)
            throw new NotValidPasswordException();

        var token = jwtService.CreateToken(
            user.Id,
            user.UserName!,
            user.Email!
        );
        var result = new IdentityUserResponseDto(
            user.UserName!,
            user.Email!,
            token
        );

        return new LoginUserResult(result);
    }
}
