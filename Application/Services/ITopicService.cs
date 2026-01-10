namespace Application.Services;

public interface ITopicService
{
    Task<Topic?> GetTopicAsync(TopicId id, CancellationToken ct);
    Task<List<Topic>> GetTopicsAsync(CancellationToken ct);
    Task<Topic> CreateTopicAsync(Topic topic, CancellationToken ct);
    Task<Topic> UpdateTopicAsync(TopicId id, Topic topic, CancellationToken ct);
    Task DeleteTopicAsync(TopicId id, CancellationToken ct);
}
