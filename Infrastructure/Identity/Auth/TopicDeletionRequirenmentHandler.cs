using Domain.Enums;

namespace Infrastructure.Identity.Auth;

public class TopicDeletionRequirement : IAuthorizationRequirement { }

public class TopicDeletionRequirementHandler(
    IApplicationDbContext dbContext,
    IHttpContextAccessor httpContextAccessor
) : AuthorizationHandler<TopicDeletionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        TopicDeletionRequirement requirement
    )
    {
        var userId = context.User.FindFirstValue("SUB");

        if (userId is null)
        {
            context.Fail();
            return;
        }

        var routeValue = httpContextAccessor.HttpContext?.Request.RouteValues;
        var value = routeValue
            ?.FirstOrDefault(x => x.Key == "id")
            .Value?.ToString();

        if (string.IsNullOrEmpty(value))
        {
            context.Fail();
            return;
        }

        var topicId = TopicId.Of(Guid.Parse(value));
        var relationship = await dbContext
            .Relationships.AsNoTracking()
            .FirstOrDefaultAsync(r =>
                r.UserId == userId && r.TopicId == topicId
            );

        if (relationship?.Role == ParticipantRole.Organizer)
        {
            context.Succeed(requirement);
        }
    }
}
