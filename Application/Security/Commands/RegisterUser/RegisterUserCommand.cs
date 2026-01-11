namespace Application.Security.Commands.RegisterUser;

public record RegisterUserCommand(RegisterUserRequestDto Object)
    : ICommand<RegisterUserResult>;

public record RegisterUserResult(IdentityUserResponseDto Object);
