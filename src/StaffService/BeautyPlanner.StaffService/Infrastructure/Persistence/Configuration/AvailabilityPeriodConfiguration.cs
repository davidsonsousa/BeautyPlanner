namespace BeautyPlanner.StaffService.Infrastructure.Persistence.Configuration;

public class AvailabilityPeriodConfiguration : IEntityTypeConfiguration<AvailabilityPeriod>
{
    public void Configure(EntityTypeBuilder<AvailabilityPeriod> b)
    {
        // Key
        b.HasKey(model => model.Id);

        // Properties
        AuditConfigurationHelper.AddAuditingConfiguration(b);

        // Relationships
        b.HasOne(wh => wh.StaffMember)
            .WithMany(sm => sm.AvailabilityPeriods)
            .HasForeignKey(wh => wh.StaffMemberId);
    }
}
