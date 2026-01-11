namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(IApplicationDbContext dbContext, IMapper mapper)
    : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    public async Task<UpdateTopicResult> Handle(
        UpdateTopicCommand request,
        CancellationToken ct
    )
    {
        var topicId = TopicId.Of(request.Id);
        var topic = await dbContext.Topics.FirstOrDefaultAsync(
            t => t.Id == topicId,
            ct
        );

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(request.Id);

        mapper.Map(request.Object, topic);

        await dbContext.SaveChangesAsync(ct);

        var result = mapper.Map<TopicResponseDto>(topic);
        return new UpdateTopicResult(result);
    }
}
