namespace Api.Security.Services;

public class JwtService(IConfiguration config) : IJwtService
{
    public string CreateToken(CustomIdentityUser user)
    {
        var secretKey = config.GetValue<string>("Auth:SecretKey");

        return secretKey;
    }
}
