namespace Application.Identity.Exceptions;

public class UserNameNotFoundException(string username)
    : UserNotFoundException(
        $"Пользователь с именем ({username}) не был найден"
    ) { }
