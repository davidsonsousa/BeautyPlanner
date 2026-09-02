namespace BeautyPlanner.CatalogService.Application.Models;

public record TreatmentResult(
    int Id,
    Guid VanityId,
    string Name,
    string? Description,
    decimal Price,
    int DurationInMinutes
);
