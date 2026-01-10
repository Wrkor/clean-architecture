namespace Application.Services;

public class TopicService(IApplicationDbContext dbContext, ILogger<TopicService> logger)
    : ITopicService
{
    public async Task<Topic?> GetTopicAsync(Guid id, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);
        var result = await dbContext
            .Topics.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);
        return result;
    }

    public async Task<List<Topic>> GetTopicsAsync(CancellationToken ct)
    {
        var result = await dbContext.Topics.AsNoTracking().ToListAsync(ct);
        return result;
    }

    public async Task<Topic> CreateTopicAsync(Topic topic, CancellationToken ct)
    {
        dbContext.Topics.Add(topic);
        await dbContext.SaveChangesAsync(ct);

        return topic;
    }

    public async Task DeleteTopicAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Topic> UpdateTopicAsync(Guid id, Topic topic, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
