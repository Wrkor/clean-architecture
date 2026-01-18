namespace Application.Comments.Dtos;

public record CreateCommentDto(Guid TopicId, string Text);
