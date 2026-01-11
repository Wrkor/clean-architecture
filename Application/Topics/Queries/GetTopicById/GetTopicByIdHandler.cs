namespace Application.Topics.Queries.GetTopicById;

public class GetTopicByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetTopicByIdQuery, GetTopicByIdResult>
{
    public async Task<GetTopicByIdResult> Handle(
        GetTopicByIdQuery request,
        CancellationToken ct
    )
    {
        var topicId = TopicId.Of(request.Id);
        var topic = await dbContext
            .Topics.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(request.Id);

        var result = topic.ToTopicResponseDto();
        return new GetTopicByIdResult(result);
    }
}
