namespace BeautyPlanner.StaffService.Application.Models;

public record CreateAvailabilityPeriodModel(
    DayOfWeek DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    Guid StaffMemberId
);
