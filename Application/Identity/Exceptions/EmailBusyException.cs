namespace Application.Identity.Exceptions;

public class EmailBusyException(string email)
    : BadRequestException($"Пользователь с email ({email}) уже существует") { }
