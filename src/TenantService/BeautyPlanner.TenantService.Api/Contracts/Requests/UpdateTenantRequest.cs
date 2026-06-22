namespace BeautyPlanner.TenantService.Api.Contracts.Requests;

public record UpdateTenantRequest(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
