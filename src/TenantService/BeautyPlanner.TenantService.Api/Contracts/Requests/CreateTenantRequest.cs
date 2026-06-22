namespace BeautyPlanner.TenantService.Api.Contracts.Requests;

public record CreateTenantRequest(
    string Name,
    string? Description
);
