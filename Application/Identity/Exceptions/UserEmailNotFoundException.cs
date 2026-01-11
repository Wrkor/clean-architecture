namespace Application.Identity.Exceptions;

public class UserEmailNotFoundException(string email)
    : UserNotFoundException($"Пользователь с email ({email}) не был найден") { }
