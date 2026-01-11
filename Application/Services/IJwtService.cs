namespace Application.Services;

public interface IJwtService
{
    string CreateToken(string id, string userName, string email);
}
