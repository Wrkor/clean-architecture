namespace Application.Topics.Dtos;

public record TopicResponseDto
{
    public Guid Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string TopicType { get; set; } = default!;
    public DateTime? EventStartedAt { get; set; } = default!;
    public LocationDto Location { get; set; } = default!;
}
