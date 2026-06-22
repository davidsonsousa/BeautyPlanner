namespace BeautyPlanner.TenantService.Application.Models;

public record UpdateTenantModel(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
