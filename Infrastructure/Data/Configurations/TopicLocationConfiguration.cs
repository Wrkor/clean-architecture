namespace Infrastructure.Data.Configurations;

public class TopicLocationConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.OwnsOne(
            p => p.Location,
            el =>
            {
                el.Property(p => p.City).HasColumnName("City");
                el.Property(p => p.Street).HasColumnName("Street");
            }
        );
    }
}
