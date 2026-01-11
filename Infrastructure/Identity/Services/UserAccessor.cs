namespace Infrastructure.Identity.Services;

public class UserAccessor(IHttpContextAccessor httpContextAccessor)
    : IUserAccessor
{
    public string GetUsername() =>
        httpContextAccessor.HttpContext!.User.FindFirstValue("NAME")!;
}
