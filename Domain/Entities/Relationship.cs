namespace Domain.Entities;

public class Relationship : Entity<RelationshipId>
{
    public ParticipantRole Role { get; set; }
    public required string UserId { get; set; } = default!;
    public required TopicId TopicId { get; set; } = default!;
    public required CustomIdentityUser User { get; set; } = default!;
    public required Topic Topic { get; set; } = default!;

    public static Relationship Create(
        RelationshipId id,
        ParticipantRole role,
        string userId,
        TopicId topicId,
        CustomIdentityUser user,
        Topic topic
    )
    {
        return new Relationship()
        {
            Id = id,
            Role = role,
            UserId = userId,
            TopicId = topicId,
            User = user,
            Topic = topic,
        };
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}
