namespace Application.Security.Exceptions;

public class UserNotFoundException(string message)
    : NotFoundException(message) { }
