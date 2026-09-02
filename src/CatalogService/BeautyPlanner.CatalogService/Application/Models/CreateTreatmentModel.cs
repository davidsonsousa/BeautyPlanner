namespace BeautyPlanner.CatalogService.Application.Models;

public record CreateTreatmentModel(
    string Name,
    string? Description,
    decimal Price,
    int DurationInMinutes,
    Guid SalonId,
    Guid ProfessionId,
    Guid TreatmentCategoryId
);
