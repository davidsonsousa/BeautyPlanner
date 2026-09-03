namespace BeautyPlanner.CatalogService.Api.Contracts.Responses;

public record TreatmentCategoryResponse(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
