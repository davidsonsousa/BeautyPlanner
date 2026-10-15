namespace BeautyPlanner.BookingService.Application.Lookups;

public record StaffLookupResult(
    Guid VanityId,
    string FirstName,
    string LastName,
    Guid ProfessionId,
    List<AvailabilityPeriodLookupResult> AvailabilityPeriods
);
