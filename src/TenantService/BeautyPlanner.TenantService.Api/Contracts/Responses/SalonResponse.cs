namespace BeautyPlanner.TenantService.Api.Contracts.Responses;

public record SalonResponse(
    int Id,
    Guid VanityId,
    string Name,
    string? Description,
    string Email,
    string PhoneNumber,
    AddressModel Address
);
