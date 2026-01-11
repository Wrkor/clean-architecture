namespace Application.Exceptions;

public class OrganizerException : ForbiddenException
{
    public OrganizerException(string message)
        : base(message) { }

    public OrganizerException(string username, object key)
        : base($"У пользователя ({username}) нет прав ({key})") { }
}
