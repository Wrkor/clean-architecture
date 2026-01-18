namespace Domain.Entities;

public class Topic : Entity<TopicId>
{
    public bool IsVoided { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string TopicType { get; set; } = default!;
    public DateTime? EventStartedAt { get; set; } = default!;
    public Location Location { get; set; } = default!;
    public List<Relationship> Users { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];

    public static Topic Create(
        TopicId id,
        string title,
        string summary,
        string topicType,
        DateTime? eventStartedAt,
        Location location
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(summary);
        ArgumentException.ThrowIfNullOrWhiteSpace(topicType);

        return new Topic()
        {
            Id = id,
            Title = title,
            Summary = summary,
            TopicType = topicType,
            EventStartedAt = eventStartedAt,
            Location = location,
        };
    }

    public bool ToggleStatus()
    {
        IsVoided = !IsVoided;

        return IsVoided;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;

        Users.ForEach(r => r.Delete());
    }
}
