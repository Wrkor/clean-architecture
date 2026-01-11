namespace Shared.CQRS;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : ICommand<TResponse>
    where TResponse : notnull { }
