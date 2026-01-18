namespace Application.Comments.Commands.CreateComment;

public record CreateCommentCommand(CreateCommentDto Object)
    : ICommand<CreateCommentResult>;

public record CreateCommentResult(CommentDto Object);
