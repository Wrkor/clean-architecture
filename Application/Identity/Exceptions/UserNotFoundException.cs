namespace Application.Identity.Exceptions;

public class UserNotFoundException(string message)
    : NotFoundException(message) { }
