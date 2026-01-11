namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler(IApplicationDbContext dbContext, IMapper mapper)
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
            .ProjectToType<TopicResponseDto>(mapper.Config)
            .ToListAsync(ct);

        return new GetTopicsResult(result);
    }
}
