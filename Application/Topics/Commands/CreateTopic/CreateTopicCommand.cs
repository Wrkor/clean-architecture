namespace Application.Topics.Commands.CreateTopic;

public record CreateTopicCommand(CreateTopicDto Object)
    : ICommand<CreateTopicResult>;

public record CreateTopicResult(TopicResponseDto Object);
