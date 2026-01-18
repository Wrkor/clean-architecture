namespace Application.Comments.Commands.CreateComment;

public class CreateCommentHandler(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IUserAccessor userAccessor
) : ICommandHandler<CreateCommentCommand, CreateCommentResult>
{
    public async Task<CreateCommentResult> Handle(
        CreateCommentCommand request,
        CancellationToken ct
    )
    {
        var topicId = TopicId.Of(request.Object.TopicId);
        var topic = await dbContext.Topics.FirstOrDefaultAsync(
            t => t.Id == topicId,
            ct
        );

        if (topic is null)
            throw new TopicNotFoundException(topicId.Value);

        var username = userAccessor.GetUsername();
        var user = await dbContext.Users.FirstOrDefaultAsync(
            u => u.UserName == username,
            ct
        );

        if (user is null)
            throw new UserNameNotFoundException(username);

        var Comment = mapper.Map<Comment>((request.Object, topic, user));
        dbContext.Comments.Add(Comment);
        await dbContext.SaveChangesAsync(ct);

        var result = mapper.Map<CommentDto>(Comment);
        return new CreateCommentResult(result);
    }
}
