namespace Infrastructure.Data.Configurations;

public class CommentIdConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, value => CommentId.Of(value));
    }
}
