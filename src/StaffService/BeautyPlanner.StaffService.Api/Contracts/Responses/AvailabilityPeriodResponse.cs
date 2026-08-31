namespace BeautyPlanner.StaffService.Api.Contracts.Responses;

public record AvailabilityPeriodResponse(
    int Id,
    Guid VanityId,
    DayOfWeek DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime
);
