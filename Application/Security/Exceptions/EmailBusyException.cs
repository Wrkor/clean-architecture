namespace Application.Security.Exceptions;

public class EmailBusyException(string email)
    : BadRequestException($"Пользователь с email ({email}) уже существует") { }
