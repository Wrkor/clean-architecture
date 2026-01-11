namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(
        CreateTopicCommand request,
        CancellationToken ct
    )
    {
        var topic = CreateTopic(request.Object);
        dbContext.Topics.Add(topic);
        await dbContext.SaveChangesAsync(ct);

        var result = topic.ToTopicResponseDto();
        return new CreateTopicResult(result);
    }

    private static Topic CreateTopic(CreateTopicDto dto)
    {
        var topicId = TopicId.Of(Guid.NewGuid());
        var location = Location.Of(dto.Location.City, dto.Location.Street);
        var topic = Topic.Create(
            topicId,
            dto.Title,
            dto.Summary,
            dto.TopicType,
            dto.EventStartedAt,
            location
        );

        return topic;
    }
}
