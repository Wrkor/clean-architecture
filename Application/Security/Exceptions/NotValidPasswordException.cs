namespace Application.Security.Exceptions;

public class NotValidPasswordException : BadRequestException
{
    public NotValidPasswordException()
        : base("Неверный пароль") { }
}
