namespace Application.Identity.Services;

public interface IJwtService
{
    string CreateToken(string id, string userName, string email);
}
