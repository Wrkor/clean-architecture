namespace Application.Identity.Commands.RegisterUser;

public class RegisterUserHandler(
    UserManager<CustomIdentityUser> manager,
    IJwtService jwtService
) : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(
        RegisterUserCommand request,
        CancellationToken ct
    )
    {
        var isUserExists = await manager.Users.AnyAsync(
            u => u.UserName == request.Object.UserName,
            ct
        );

        if (isUserExists)
            throw new UsernameBusyException(request.Object.UserName);

        isUserExists = await manager.Users.AnyAsync(
            u => u.Email == request.Object.Email,
            ct
        );

        if (isUserExists)
            throw new EmailBusyException(request.Object.Email);

        var user = new CustomIdentityUser()
        {
            FullName = request.Object.FullName,
            Email = request.Object.Email,
            UserName = request.Object.UserName,
            About = string.Empty,
        };

        var identityResult = await manager.CreateAsync(
            user,
            request.Object.Password
        );

        if (!identityResult.Succeeded)
            throw new NotValidPasswordException();

        var token = jwtService.CreateToken(user.Id, user.UserName, user.Email);
        var result = new IdentityUserResponseDto(
            user.UserName,
            user.Email,
            token
        );

        return new RegisterUserResult(result);
    }
}
