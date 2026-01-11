namespace Application.Topics.Commands.DeleteTopic;

public class DeleteTopicHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteTopicCommand, DeleteTopicResult>
{
    public async Task<DeleteTopicResult> Handle(
        DeleteTopicCommand request,
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

        topic.IsDeleted = true;
        topic.DeletedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);

        return new DeleteTopicResult();
    }
}
