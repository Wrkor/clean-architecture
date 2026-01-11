namespace Api.Security.Services;

public interface IJwtService
{
    string CreateToken(CustomIdentityUser user);
}
