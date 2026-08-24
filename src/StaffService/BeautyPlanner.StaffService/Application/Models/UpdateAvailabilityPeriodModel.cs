namespace BeautyPlanner.StaffService.Application.Models;

public record UpdateAvailabilityPeriodModel(
    Guid VanityId,
    DayOfWeek DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    Guid StaffMemberId
);
