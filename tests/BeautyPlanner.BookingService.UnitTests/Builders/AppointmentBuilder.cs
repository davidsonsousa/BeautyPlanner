namespace BeautyPlanner.BookingService.UnitTests.Builders;

public class AppointmentBuilder
{
    private readonly int _id = 999;
    private readonly Guid _vanityId = Guid.Empty;
    private readonly Guid _clientVanityId = Guid.Empty;
    private readonly Guid _staffMemberVanityId = Guid.Empty;
    private readonly Guid _treatmentVanityId = Guid.Empty;
    private readonly Guid _salonVanityId = Guid.Empty;
    private TimeRange _timeRange = new(new DateTime(2026, 11, 9, 10, 0, 0), new DateTime(2026, 11, 9, 10, 30, 0));

    public AppointmentBuilder WithTimeRange(TimeRange timerange)
    {
        _timeRange = timerange;
        return this;
    }

    public Appointment Build()
    {
        return new Appointment(_timeRange, _clientVanityId, _staffMemberVanityId, _treatmentVanityId, _salonVanityId);
    }
}
