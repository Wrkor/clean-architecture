namespace Domain.Entities;

public class Comment : Entity<CommentId>
{
    public string Text { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = default!;
    public Topic Topic { get; set; } = default!;
    public CustomIdentityUser Author { get; set; } = default!;

    public static Comment Create(
        CommentId id,
        string text,
        Topic topic,
        CustomIdentityUser author
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        return new Comment()
        {
            Id = id,
            Text = text,
            Topic = topic,
            Author = author,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
