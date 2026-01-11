namespace Application.Security.Exceptions;

public class UsernameBusyException(string username)
    : BadRequestException(
        $"Пользователь с именем ({username}) уже существует"
    ) { }
