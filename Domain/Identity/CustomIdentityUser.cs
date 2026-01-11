namespace Domain.Identity;

public class CustomIdentityUser : IdentityUser
{
    public string FullName { get; set; } = default!;
    public string About { get; set; } = default!;
    public List<Relationship> Topics { get; set; } = [];
}
