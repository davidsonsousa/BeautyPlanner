namespace BeautyPlanner.CatalogService.Api.Contracts.Requests;

public record CreateTreatmentRequest(
    string Name,
    string? Description,
    decimal Price,
    int DurationInMinutes,
    Guid SalonId,
    Guid ProfessionId,
    Guid TreatmentCategoryId
);
