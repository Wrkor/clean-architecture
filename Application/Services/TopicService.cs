namespace Application.Services;

public class TopicService(IApplicationDbContext dbContext, ILogger<TopicService> logger)
    : ITopicService
{
    public async Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);
        var result = await dbContext
            .Topics.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);

        if (result is null)
            throw new TopicNotFoundException(id);

        return result.ToTopicResponseDto();
    }

    public async Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct)
    {
        var result = await dbContext.Topics.AsNoTracking().ToListAsync(ct);
        return result.ToTopicResponseDtoList();
    }

    public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto topic, CancellationToken ct)
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

    public async Task DeleteTopicAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<TopicResponseDto> UpdateTopicAsync(Guid id, Topic topic, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
