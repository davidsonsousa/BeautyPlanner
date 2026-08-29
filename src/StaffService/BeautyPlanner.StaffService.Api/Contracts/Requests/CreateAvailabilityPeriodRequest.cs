namespace BeautyPlanner.StaffService.Api.Contracts.Requests;

public record CreateAvailabilityPeriodRequest(
    DayOfWeek DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime
);
