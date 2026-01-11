namespace Infrastructure.Data.Configurations;

public class RelationshipRoleConfiguration
    : IEntityTypeConfiguration<Relationship>
{
    public void Configure(EntityTypeBuilder<Relationship> builder)
    {
        builder.Property(p => p.Role).HasConversion<string>();
    }
}
