namespace Infrastructure.Data.Configurations;

public class RelationshipIdConfiguration
    : IEntityTypeConfiguration<Relationship>
{
    public void Configure(EntityTypeBuilder<Relationship> builder)
    {
        builder
            .Property(p => p.Id)
            .HasConversion(id => id.Value, value => RelationshipId.Of(value));
    }
}
