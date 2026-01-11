namespace Application.Security.Queries.LoginUser;

public record LoginUserQuery(LoginRequestDto Object) : IQuery<LoginUserResult>;

public record LoginUserResult(IdentityUserResponseDto Object);
