namespace Application.Services;

public class TopicService(IApplicationDbContext dbContext)
{
    public async Task<TopicResponseDto> CreateTopicAsync(
        CreateTopicDto topic,
        CancellationToken ct
    )
    {
        var topicId = TopicId.Of(Guid.NewGuid());
        var location = Location.Of(topic.Location.City, topic.Location.Street);
        var topicCreated = Topic.Create(
            topicId,
            topic.Title,
            topic.Summary,
            topic.TopicType,
            topic.EventStartedAt,
            location
        );
        dbContext.Topics.Add(topicCreated);
        await dbContext.SaveChangesAsync(ct);

        return topicCreated.ToTopicResponseDto();
    }

    public async Task<TopicResponseDto> UpdateTopicAsync(
        Guid id,
        UpdateTopicDto topic,
        CancellationToken ct
    )
    {
        var topicId = TopicId.Of(id);
        var topicUpdated = await dbContext.Topics.FirstOrDefaultAsync(
            t => t.Id == topicId,
            ct
        );

        if (topicUpdated is null || topicUpdated.IsDeleted)
            throw new TopicNotFoundException(id);

        topicUpdated.Title = topic.Title ?? topicUpdated.Title;
        topicUpdated.Summary = topic.Summary ?? topicUpdated.Summary;
        topicUpdated.TopicType = topic.TopicType ?? topicUpdated.TopicType;
        topicUpdated.EventStartedAt = topic.EventStartedAt;
        topicUpdated.Location = Location.Of(
            topic.Location.City,
            topic.Location.Street
        );

        await dbContext.SaveChangesAsync(ct);

        return topicUpdated.ToTopicResponseDto();
    }

    public async Task DeleteTopicAsync(Guid id, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);
        var topic = await dbContext.Topics.FirstOrDefaultAsync(
            t => t.Id == topicId,
            ct
        );

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        topic.IsDeleted = true;
        topic.DeletedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);
    }
}
