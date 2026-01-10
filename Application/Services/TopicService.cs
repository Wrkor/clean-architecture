using Application.Extensions;

namespace Application.Services;

public class TopicService(IApplicationDbContext dbContext, ILogger<TopicService> logger)
    : ITopicService
{
    public async Task<TopicResponseDto?> GetTopicAsync(Guid id, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);
        var result = await dbContext
            .Topics.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);
        return result?.ToTopicResponseDto();
    }

    public async Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct)
    {
        var result = await dbContext.Topics.AsNoTracking().ToListAsync(ct);
        return result.ToTopicResponseDtoList();
    }

    public async Task<TopicResponseDto> CreateTopicAsync(Topic topic, CancellationToken ct)
    {
        dbContext.Topics.Add(topic);
        await dbContext.SaveChangesAsync(ct);

        return topic.ToTopicResponseDto();
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
