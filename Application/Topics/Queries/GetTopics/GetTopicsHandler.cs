namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetTopicsQuery, GetTopicsResult>
{
    public async Task<GetTopicsResult> Handle(
        GetTopicsQuery request,
        CancellationToken ct
    )
    {
        var result = await dbContext
            .Topics.Where(t => !t.IsDeleted)
            .AsNoTracking()
            .ProjectToType<TopicResponseDto>()
            .ToListAsync(ct);

        return new GetTopicsResult(result);
    }
}
