namespace BeautyPlanner.TenantService.Api.Contracts.Requests;

public record UpdateSalonRequest(
    int Id,
    Guid VanityId,
    string Name,
    string? Description,
    string Email,
    string PhoneNumber,
    AddressModel Address,
    Guid TenantId
);
