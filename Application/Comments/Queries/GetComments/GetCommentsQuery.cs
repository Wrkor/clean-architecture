namespace Application.Comments.Queries.GetComments;

public record GetCommentsQuery(Guid TopicId) : IQuery<GetCommentsResult>;

public record GetCommentsResult(List<CommentDto> Objects);
