namespace BeautyPlanner.CatalogService.Application.Models;

public record TreatmentCategoryResult(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
