namespace Infrastructure.Data.Configurations;

public class RelationshipNavigationConfiguration
    : IEntityTypeConfiguration<Relationship>
{
    public void Configure(EntityTypeBuilder<Relationship> builder)
    {
        builder
            .HasOne(p => p.Topic)
            .WithMany(t => t.Users)
            .HasForeignKey(p => p.TopicId);

        builder
            .HasOne(p => p.User)
            .WithMany(u => u.Topics)
            .HasForeignKey(p => p.UserId);
    }
}
