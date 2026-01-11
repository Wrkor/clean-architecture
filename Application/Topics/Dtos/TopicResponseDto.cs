namespace Application.Topics.Dtos;

public record TopicResponseDto(
    Guid Id,
    string Title,
    string Summary,
    string TopicType,
    DateTime? EventStartedAt,
    LocationDto Location,
    List<UserDto> Users
);
