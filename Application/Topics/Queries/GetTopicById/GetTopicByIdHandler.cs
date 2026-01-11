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
        var result = await dbContext
            .Topics.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);

        if (result is null || result.IsDeleted)
            throw new TopicNotFoundException(request.Id);

        return new GetTopicByIdResult(result.ToTopicResponseDto());
    }
}
