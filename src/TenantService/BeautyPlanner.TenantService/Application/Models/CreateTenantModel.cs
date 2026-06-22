namespace BeautyPlanner.TenantService.Application.Models;

public record CreateTenantModel(
    string Name,
    string? Description
);
