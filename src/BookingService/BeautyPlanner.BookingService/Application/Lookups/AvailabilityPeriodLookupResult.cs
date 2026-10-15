namespace BeautyPlanner.BookingService.Application.Lookups;

public record AvailabilityPeriodLookupResult(
    int Id,
    Guid VanityId,
    DayOfWeek DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime
);