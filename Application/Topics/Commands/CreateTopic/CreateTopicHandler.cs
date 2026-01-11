namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler(IApplicationDbContext dbContext, IMapper mapper)
    : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(
        CreateTopicCommand request,
        CancellationToken ct
    )
    {
        var topic = mapper.Map<Topic>(request.Object);
        dbContext.Topics.Add(topic);
        await dbContext.SaveChangesAsync(ct);

        var result = mapper.Map<TopicResponseDto>(topic);
        return new CreateTopicResult(result);
    }
}
