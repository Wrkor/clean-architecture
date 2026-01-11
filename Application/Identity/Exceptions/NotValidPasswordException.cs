namespace Application.Identity.Exceptions;

public class NotValidPasswordException()
    : BadRequestException("Неверный пароль") { }
