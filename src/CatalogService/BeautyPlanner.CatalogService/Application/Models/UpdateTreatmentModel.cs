namespace BeautyPlanner.CatalogService.Application.Models;

public record UpdateTreatmentModel(
    int Id,
    Guid VanityId,
    string Name,
    string? Description,
    decimal Price,
    int DurationInMinutes,
    Guid ProfessionId,
    Guid TreatmentCategoryId
);
