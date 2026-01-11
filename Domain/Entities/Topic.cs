namespace Domain.Entities;

public class Topic : Entity<TopicId>
{
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string TopicType { get; set; } = default!;
    public DateTime? EventStartedAt { get; set; } = default!;
    public Location Location { get; set; } = default!;

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

    public void Update(
        string title,
        string summary,
        string topicType,
        string city,
        string street,
        DateTime? eventStartedAt
    )
    {
        Title = title ?? Title;
        Summary = summary ?? Summary;
        TopicType = topicType ?? TopicType;
        EventStartedAt = eventStartedAt;
        Location = Location.Of(city, street);
    }
}
