namespace BeautyPlanner.StaffService.UnitTests.Builders;

public class AvailabilityPeriodBuilder
{
    private Guid _vanityId = Guid.Empty;
    private DayOfWeek _dayOfWeek = DayOfWeek.Monday;
    private TimeSpan _startTime = TimeSpan.FromHours(9);
    private TimeSpan _endTime = TimeSpan.FromHours(18);
    private int _staffMemberId = 999;
    private Guid _staffMemberVanityId = Guid.Empty;

    public AvailabilityPeriodBuilder WithDayOfWeek(DayOfWeek dayOfWeek)
    {
        _dayOfWeek = dayOfWeek;
        return this;
    }

    public AvailabilityPeriodBuilder WithStartTime(TimeSpan startTime)
    {
        _startTime = startTime;
        return this;
    }

    public AvailabilityPeriodBuilder WithEndTime(TimeSpan endTime)
    {
        _endTime = endTime;
        return this;
    }

    public AvailabilityPeriod Build()
    {
        return new AvailabilityPeriod(_dayOfWeek, _startTime, _endTime, _staffMemberId);
    }

    public CreateAvailabilityPeriodModel BuildCreateModel()
    {
        return new CreateAvailabilityPeriodModel(_dayOfWeek, _startTime, _endTime, _staffMemberVanityId);
    }

    public UpdateAvailabilityPeriodModel BuildUpdateModel()
    {
        return new UpdateAvailabilityPeriodModel(_vanityId, _dayOfWeek, _startTime, _endTime, _staffMemberVanityId);
    }
}
