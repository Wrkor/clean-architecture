namespace Application.Comments.Dtos;

public record CommentDto(
    Guid Id,
    string Text,
    string UserName,
    string FullName,
    DateTime CreatedAt
);
