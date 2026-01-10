namespace Application.Services;

public interface ITopicService
{
    Task<Topic?> GetTopicAsync(Guid id, CancellationToken ct);
    Task<List<Topic>> GetTopicsAsync(CancellationToken ct);
    Task<Topic> CreateTopicAsync(Topic topic, CancellationToken ct);
    Task<Topic> UpdateTopicAsync(Guid id, Topic topic, CancellationToken ct);
    Task DeleteTopicAsync(Guid id, CancellationToken ct);
}
