namespace Application.Topics.Commands.JoinLeaveTopic;

public class JoinLeaveTopicHandler(
    IApplicationDbContext dbContext,
    IUserAccessor userAccessor
) : ICommandHandler<JoinLeaveTopicCommand, JoinLeaveTopicResult>
{
    public async Task<JoinLeaveTopicResult> Handle(
        JoinLeaveTopicCommand request,
        CancellationToken ct
    )
    {
        var user = await GetUserAsync(ct);
        var topic = await GetTopicAsync(request.Id, ct);
        var organizer = topic
            .Users.FirstOrDefault(u =>
                u.Role == ParticipantRole.Organizer
                && u.User.UserName == user.UserName
            )
            ?.User;

        var result = string.Empty;
        if (organizer is null)
        {
            var joinUser = topic.Users.FirstOrDefault(u =>
                u.User.UserName == user.UserName
            );

            if (joinUser is null)
            {
                var relationshipId = RelationshipId.Of(Guid.NewGuid());
                var relationship = Relationship.Create(
                    id: relationshipId,
                    role: ParticipantRole.Participant,
                    userId: user.Id,
                    topicId: topic.Id,
                    user: user,
                    topic: topic
                );

                topic.Users.Add(relationship);
                result = $"Вы присоединились ({topic.Id.Value})";
            }
            else
            {
                topic.Users.Remove(joinUser);
                result = $"Вы покинули ({topic.Id.Value})";
            }
        }
        else
        {
            var oldStatus = topic.IsVoided;
            var newStatus = topic.ToggleStatus();
            result = $"Статус изменился: {oldStatus} -> {newStatus}";
        }

        dbContext.Topics.Update(topic);
        await dbContext.SaveChangesAsync(ct);
        return new JoinLeaveTopicResult(result);
    }

    private async Task<CustomIdentityUser> GetUserAsync(CancellationToken ct)
    {
        var username = userAccessor.GetUsername();
        var user = await dbContext.Users.FirstOrDefaultAsync(
            u => u.UserName == username,
            ct
        );

        if (user is null)
            throw new UserNameNotFoundException(username);

        return user;
    }

    private async Task<Topic> GetTopicAsync(Guid id, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);
        var topic = await dbContext
            .Topics.Include(t => t.Users)
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);

        if (topic is null || topic.IsDeleted)
            throw new TopicNotFoundException(id);

        return topic;
    }
}
