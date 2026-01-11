namespace Application.Security.Exceptions;

public class NotValidPasswordException()
    : BadRequestException("Неверный пароль") { }
