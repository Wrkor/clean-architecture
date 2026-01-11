namespace Application.Topics.Queries.GetTopicById;

public class GetTopicByIdHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IQueryHandler<GetTopicByIdQuery, GetTopicByIdResult>
{
    public async Task<GetTopicByIdResult> Handle(
        GetTopicByIdQuery request,
        CancellationToken ct
    )
    {
        var topicId = TopicId.Of(request.Id);
        var result = await dbContext
            .Topics.Where(t => t.Id == topicId && !t.IsDeleted)
            .AsNoTracking()
            .ProjectToType<TopicResponseDto>(mapper.Config)
            .FirstOrDefaultAsync(ct);

        if (result is null)
            throw new TopicNotFoundException(request.Id);

        return new GetTopicByIdResult(result);
    }
}
