namespace Application.Topics.Queries.GetTopicById;

public record GetTopicByIdQuery(Guid Id) : IQuery<GetTopicByIdResult>;

public record GetTopicByIdResult(TopicResponseDto Object);
