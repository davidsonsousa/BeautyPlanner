namespace BeautyPlanner.TenantService.Infrastructure.Persistence.Configuration;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> b)
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
        b.HasMany(model => model.Salons);
    }
}
