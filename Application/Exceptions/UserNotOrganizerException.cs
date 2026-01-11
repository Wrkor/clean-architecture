namespace Application.Exceptions;

public class UserNotOrganizerException(string username, Guid topicId)
    : OrganizerException(username, topicId) { }
