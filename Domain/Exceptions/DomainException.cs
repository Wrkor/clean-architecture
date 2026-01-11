namespace Domain.Exceptions;

public class DomainException(string message)
    : Exception($"Domain exceptions: ({message}).") { }
