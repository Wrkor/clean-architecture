namespace Infrastructure.Data.Configurations;

public class TopicIdConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.Property(p => p.Id).HasConversion(id => id.Value, value => TopicId.Of(value));
    }
}
