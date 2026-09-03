namespace BeautyPlanner.CatalogService.Api.Contracts.Responses;

public record TreatmentResponse(
    int Id,
    Guid VanityId,
    string Name,
    string? Description,
    decimal Price,
    int DurationInMinutes,
    Guid ProfessionId,
    string TreatmentCategory
);
