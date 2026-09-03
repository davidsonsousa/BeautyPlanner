namespace BeautyPlanner.CatalogService.Api.Contracts.Requests;

public record UpdateTreatmentRequest(
    int Id,
    Guid VanityId,
    string Name,
    string? Description,
    decimal Price,
    int DurationInMinutes,
    Guid ProfessionId,
    Guid TreatmentCategoryId
);
