namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IUserAccessor userAccessor
) : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(
        CreateTopicCommand request,
        CancellationToken ct
    )
    {
        var username = userAccessor.GetUsername();
        var user = await dbContext
            .Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username, ct);

        if (user is null)
            throw new UserNameNotFoundException(username);

        var topic = mapper.Map<Topic>(request.Object);
        var relationshipId = RelationshipId.Of(Guid.NewGuid());
        var relationship = Relationship.Create(
            id: relationshipId,
            role: ParticipantRole.Organizer,
            userId: user.Id,
            topicId: topic.Id,
            topic: topic,
            user: user
        );
        topic.Users.Add(relationship);
        dbContext.Topics.Add(topic);
        await dbContext.SaveChangesAsync(ct);

        var result = mapper.Map<TopicResponseDto>(topic);
        return new CreateTopicResult(result);
    }
}
