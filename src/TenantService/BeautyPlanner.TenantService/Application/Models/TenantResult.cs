namespace BeautyPlanner.TenantService.Application.Models;

public record TenantResult(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
