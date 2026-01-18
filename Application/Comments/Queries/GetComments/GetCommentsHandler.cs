namespace Application.Comments.Queries.GetComments;

public class GetCommentsHandler(IApplicationDbContext dbContext, IMapper mapper)
    : IQueryHandler<GetCommentsQuery, GetCommentsResult>
{
    public async Task<GetCommentsResult> Handle(
        GetCommentsQuery request,
        CancellationToken ct
    )
    {
        var topicId = TopicId.Of(request.TopicId);
        var result = await dbContext
            .Comments.Where(c => !c.IsDeleted && c.Topic.Id == topicId)
            .OrderByDescending(c => c.CreatedAt)
            .ProjectToType<CommentDto>(mapper.Config)
            .AsNoTracking()
            .ToListAsync(ct);

        return new GetCommentsResult(result);
    }
}
