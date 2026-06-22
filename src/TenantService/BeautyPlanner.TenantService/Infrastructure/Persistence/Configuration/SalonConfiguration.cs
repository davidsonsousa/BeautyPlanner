namespace BeautyPlanner.TenantService.Infrastructure.Persistence.Configuration;

public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> b)
    {
        // Key
        b.HasKey(model => model.Id);

        // Properties
        b.Property(model => model.Name)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.Description)
            .HasMaxLength(500);

        b.Property(model => model.Email)
            .IsRequired()
            .HasMaxLength(255);

        b.Property(model => model.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        b.OwnsOne(model => model.Address, a => AddressConfigurationHelper.AddAddressConfiguration(a));

        AuditConfigurationHelper.AddAuditingConfiguration(b);

        // Relationships
        b.HasOne(s => s.Tenant)
            .WithMany(t => t.Salons)
            .HasForeignKey(s => s.TenantId);
    }
}
