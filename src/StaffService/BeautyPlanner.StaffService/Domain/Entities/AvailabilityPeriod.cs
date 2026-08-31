namespace BeautyPlanner.StaffService.Domain.Entities;

public class AvailabilityPeriod : AuditableEntity
{
    public AvailabilityPeriod(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime, int staffMemberId)
    {
        SetPropertyValues(dayOfWeek, startTime, endTime);

        StaffMemberId = staffMemberId;
    }

    public DayOfWeek DayOfWeek { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public int StaffMemberId { get; set; }

    public StaffMember StaffMember { get; set; } = null!;

    public void Update(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        SetPropertyValues(dayOfWeek, startTime, endTime);
    }

    private void SetPropertyValues(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
    }
}
