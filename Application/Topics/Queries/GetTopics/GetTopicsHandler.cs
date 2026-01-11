namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetTopicsQuery, GetTopicResult>
{
    public async Task<GetTopicResult> Handle(
        GetTopicsQuery request,
        CancellationToken ct
    )
    {
        var result = await dbContext
            .Topics.Where(t => !t.IsDeleted)
            .AsNoTracking()
            .ToListAsync(ct);

        return new GetTopicResult(result.ToTopicResponseDtoList());
    }
}
