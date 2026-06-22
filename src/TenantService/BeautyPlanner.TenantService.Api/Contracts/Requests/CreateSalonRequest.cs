namespace BeautyPlanner.TenantService.Api.Contracts.Requests;

public record CreateSalonRequest(
    string Name,
    string? Description,
    string Email,
    string PhoneNumber,
    AddressModel Address,
    Guid TenantId
);
