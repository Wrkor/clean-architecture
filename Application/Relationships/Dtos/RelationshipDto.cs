namespace Application.Relationships.Dtos;

public record RelationshipDto(
    RelationshipId Id,
    string UserId,
    TopicId TopicId,
    TopicResponseDto Topic,
    UserDto User
);
