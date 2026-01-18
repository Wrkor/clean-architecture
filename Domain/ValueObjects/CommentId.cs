namespace Domain.ValueObjects;

public record CommentId
{
    public Guid Value { get; }

    private CommentId(Guid value)
    {
        Value = value;
    }

    public static CommentId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException(
                $"{nameof(CommentId)} не может быть пустым"
            );

        return new CommentId(value);
    }
}
