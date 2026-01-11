namespace Application.Identity.Mapping;

public class IdentityRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Relationship, UserDto>()
            .Map(dest => dest.Id, src => src.User.Id)
            .Map(dest => dest.UserName, src => src.User.UserName)
            .Map(dest => dest.FullName, src => src.User.FullName)
            .Map(dest => dest.Role, src => src.Role.ToString());
    }
}
