namespace BeautyPlanner.BookingService.Infrastructure.Persistence.Configuration;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> b)
    {
        // Key
        b.HasKey(model => model.Id);

        // Properties
        b.Property(model => model.Status)
            .IsRequired();

        b.OwnsOne(model => model.Period, tr =>
        {
            tr.Property(x => x.StartTime)
                .IsRequired();

            tr.Property(x => x.EndTime)
                .IsRequired();
        });

        AuditConfigurationHelper.AddAuditingConfiguration(b);
    }
}
