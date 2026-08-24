namespace BeautyPlanner.StaffService.Infrastructure.Persistence.Configuration;

public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
{
    public void Configure(EntityTypeBuilder<Profession> b)
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
        b.HasMany(p => p.StaffMembers)
            .WithOne(sm => sm.Profession)
            .HasForeignKey(sm => sm.ProfessionId);
    }
}
