namespace Application.Topics.Commands.UpdateTopic;

public record UpdateTopicCommand(Guid Id, UpdateTopicDto Object)
    : ICommand<UpdateTopicResult>;

public record UpdateTopicResult(TopicResponseDto Object);
