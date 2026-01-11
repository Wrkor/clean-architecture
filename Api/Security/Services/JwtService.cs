namespace Api.Security.Services;

public class JwtService(IConfiguration config) : IJwtService
{
    public string CreateToken(CustomIdentityUser user)
    {
        var secretKey = config.GetValue<string>("Auth:SecretKey")!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha512Signature
        );
        var claims = new List<Claim>
        {
            new("SUB", user.Id),
            new("NAME", user.UserName!),
            new("EMAIL", user.Email!),
        };
        var identity = new ClaimsIdentity(claims);
        var descriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = creds,
            Subject = identity,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(10),
        };
        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(descriptor);

        return token;
    }
}
