namespace BeautyPlanner.CatalogService.Infrastructure.Persistence.Configuration;

public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
{
    public void Configure(EntityTypeBuilder<Treatment> b)
    {
        // Key
        b.HasKey(model => model.Id);

        // Properties
        b.Property(model => model.Name)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.Description)
            .HasMaxLength(500);

        b.Property(model => model.Price)
            .IsRequired();

        b.Property(model => model.DurationInMinutes)
            .IsRequired();

        AuditConfigurationHelper.AddAuditingConfiguration(b);

        // Relationships
        b.HasOne(t => t.TreatmentCategory)
            .WithMany(tc => tc.Treatments)
            .HasForeignKey(t => t.TreatmentCategoryId);
    }
}
