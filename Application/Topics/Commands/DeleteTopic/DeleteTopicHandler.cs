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
        var topic = await dbContext
            .Topics.Include(r => r.Users)
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(request.Id);

        topic.Delete();

        await dbContext.SaveChangesAsync(ct);

        return new DeleteTopicResult();
    }
}
