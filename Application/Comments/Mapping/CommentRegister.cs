namespace Application.Comments.Mapping;

public class CommentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CommentId, Guid>().MapWith(src => src.Value);
        config.NewConfig<Guid, CommentId>().MapWith(src => CommentId.Of(src));
        config
            .NewConfig<Comment, CommentDto>()
            .Map(dest => dest.UserName, src => src.Author.UserName)
            .Map(dest => dest.FullName, src => src.Author.FullName);
        config
            .NewConfig<
                (
                    CreateCommentDto Comment,
                    Topic Topic,
                    CustomIdentityUser User
                ),
                Comment
            >()
            .MapWith(src =>
                Comment.Create(
                    CommentId.Of(Guid.NewGuid()),
                    src.Comment.Text,
                    src.Topic,
                    src.User
                )
            );
    }
}
