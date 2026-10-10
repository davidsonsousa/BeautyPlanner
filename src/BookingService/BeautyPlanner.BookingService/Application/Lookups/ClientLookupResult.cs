namespace BeautyPlanner.BookingService.Application.Lookups;

public record ClientLookupResult(
    Guid VanityId,
    string FirstName,
    string LastName
);
