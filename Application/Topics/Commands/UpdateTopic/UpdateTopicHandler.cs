namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(IApplicationDbContext dbContext)
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

        topic.Update(
            request.Object.Title,
            request.Object.Summary,
            request.Object.TopicType,
            request.Object.Location.City,
            request.Object.Location.Street,
            request.Object.EventStartedAt
        );

        await dbContext.SaveChangesAsync(ct);

        var result = topic.ToTopicResponseDto();
        return new UpdateTopicResult(result);
    }
}
