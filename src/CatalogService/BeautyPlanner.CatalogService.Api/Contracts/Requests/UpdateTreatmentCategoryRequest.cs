namespace BeautyPlanner.CatalogService.Api.Contracts.Requests;

public record UpdateTreatmentCategoryRequest(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
