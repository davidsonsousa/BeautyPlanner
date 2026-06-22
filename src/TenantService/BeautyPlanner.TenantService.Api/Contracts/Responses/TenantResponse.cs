namespace BeautyPlanner.TenantService.Api.Contracts.Responses;

public record TenantResponse(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
