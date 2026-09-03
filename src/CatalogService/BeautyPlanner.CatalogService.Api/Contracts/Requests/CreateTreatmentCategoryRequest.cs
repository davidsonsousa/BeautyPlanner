namespace BeautyPlanner.CatalogService.Api.Contracts.Requests;

public record CreateTreatmentCategoryRequest(
    string Name,
    string? Description
);
