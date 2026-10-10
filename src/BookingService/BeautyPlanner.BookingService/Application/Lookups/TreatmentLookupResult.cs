namespace BeautyPlanner.BookingService.Application.Lookups;

public record TreatmentLookupResult(
    Guid VanityId,
    string Name,
    decimal Price,
    int DurationInMinutes
);
