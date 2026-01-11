namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(
    IApplicationDbContext dbContext,
    IMapper mapper,
    IUserAccessor userAccessor
) : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    public async Task<UpdateTopicResult> Handle(
        UpdateTopicCommand request,
        CancellationToken ct
    )
    {
        var topicId = TopicId.Of(request.Id);
        var topic = await dbContext
            .Topics.Include(t => t.Users)
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(request.Id);

        var username = userAccessor.GetUsername();
        var isUserOrganizer = topic.Users.Any(u =>
            u.Role == ParticipantRole.Organizer && u.User.UserName == username
        );

        if (!isUserOrganizer)
            throw new UserNotOrganizerException(username, request.Id);

        mapper.Map(request.Object, topic);
        await dbContext.SaveChangesAsync(ct);

        var result = mapper.Map<TopicResponseDto>(topic);
        return new UpdateTopicResult(result);
    }
}
