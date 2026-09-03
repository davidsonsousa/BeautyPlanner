namespace BeautyPlanner.CatalogService.Infrastructure.Persistence.Configuration;

public class TreatmentCategoryConfiguration : IEntityTypeConfiguration<TreatmentCategory>
{
    public void Configure(EntityTypeBuilder<TreatmentCategory> b)
    {
        // Key
        b.HasKey(model => model.Id);

        // Properties
        b.Property(model => model.Name)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.Description)
            .HasMaxLength(500);

        AuditConfigurationHelper.AddAuditingConfiguration(b);

        // Relationships
        b.HasMany(tc => tc.Treatments)
            .WithOne(t => t.TreatmentCategory)
            .HasForeignKey(t => t.TreatmentCategoryId);
    }
}
