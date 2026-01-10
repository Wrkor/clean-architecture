namespace Application.Services;

public interface ITopicService
{
    Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct);
    Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct);
    Task<TopicResponseDto> CreateTopicAsync(Topic topic, CancellationToken ct);
    Task<TopicResponseDto> UpdateTopicAsync(Guid id, Topic topic, CancellationToken ct);
    Task DeleteTopicAsync(Guid id, CancellationToken ct);
}
