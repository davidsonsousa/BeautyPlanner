namespace BeautyPlanner.CatalogService.Application.Models;

public record UpdateTreatmentCategoryModel(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
