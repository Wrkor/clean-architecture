namespace Application.Topics.Queries.GetTopics;

public record GetTopicsQuery : IQuery<GetTopicResult>;

public record GetTopicResult(List<TopicResponseDto> Objects);
