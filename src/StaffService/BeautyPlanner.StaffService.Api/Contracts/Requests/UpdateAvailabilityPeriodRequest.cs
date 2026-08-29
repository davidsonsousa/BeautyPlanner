namespace BeautyPlanner.StaffService.Api.Contracts.Requests;

public record UpdateAvailabilityPeriodRequest(
    Guid VanityId,
    DayOfWeek DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    Guid StaffMemberId
);
