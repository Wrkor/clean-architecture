namespace Application.Services;

public class TopicService(IApplicationDbContext dbContext, ILogger<TopicService> logger)
    : ITopicService
{
    public async Task<Topic?> GetTopicAsync(TopicId id, CancellationToken ct)
    {
        var result = await dbContext.Topics.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, ct);
        return result;
    }

    public async Task<List<Topic>> GetTopicsAsync(CancellationToken ct)
    {
        var result = await dbContext.Topics.ToListAsync(ct);
        return result;
    }

    public async Task<Topic> CreateTopicAsync(Topic topic, CancellationToken ct)
    {
        dbContext.Topics.Add(topic);
        await dbContext.SaveChangesAsync(ct);

        return topic;
    }

    public async Task DeleteTopicAsync(TopicId id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Topic> UpdateTopicAsync(TopicId id, Topic topic, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
