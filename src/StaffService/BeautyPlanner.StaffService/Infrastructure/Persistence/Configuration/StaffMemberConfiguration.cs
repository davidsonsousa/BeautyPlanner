namespace BeautyPlanner.StaffService.Infrastructure.Persistence.Configuration;

public class StaffMemberConfiguration : IEntityTypeConfiguration<StaffMember>
{
    public void Configure(EntityTypeBuilder<StaffMember> b)
    {
        // Key
        b.HasKey(model => model.Id);

        // Properties
        b.Property(model => model.FirstName)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.LastName)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.Email)
            .HasMaxLength(150)
            .IsRequired();

        b.Property(model => model.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();

        b.Property(model => model.DateOfBirth)
            .HasMaxLength(9);

        b.OwnsOne(model => model.Address, a => AddressConfigurationHelper.AddAddressConfiguration(a));

        AuditConfigurationHelper.AddAuditingConfiguration(b);

        // Relationships
        b.HasOne(sm => sm.Profession)
            .WithMany(p => p.StaffMembers)
            .HasForeignKey(sm => sm.ProfessionId);

        b.HasMany(model => model.AvailabilityPeriods)
            .WithOne(wh => wh.StaffMember)
            .HasForeignKey(wh => wh.StaffMemberId);
    }
}
