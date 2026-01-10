namespace Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public required T Id { get; set; }
    public bool IsDeleted { get; set; } = default!;
    public DateTime? DeletedAt { get; set; } = default!;
}
