namespace BeautyPlanner.StaffService.Application.Models;

public record AvailabilityPeriodResult(
    int Id,
    Guid VanityId,
    DayOfWeek DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime
);
