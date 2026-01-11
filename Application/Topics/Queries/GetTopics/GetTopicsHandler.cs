namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetTopicsQuery, GetTopicsResult>
{
    public async Task<GetTopicsResult> Handle(
        GetTopicsQuery request,
        CancellationToken ct
    )
    {
        var topics = await dbContext
            .Topics.Where(t => !t.IsDeleted)
            .AsNoTracking()
            .ToListAsync(ct);

        var result = topics.ToTopicResponseDtoList();
        return new GetTopicsResult(result);
    }
}
