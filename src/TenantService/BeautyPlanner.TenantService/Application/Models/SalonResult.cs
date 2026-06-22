namespace BeautyPlanner.TenantService.Application.Models;

public record SalonResult(
    int Id,
    Guid VanityId,
    string Name,
    string? Description,
    string Email,
    string PhoneNumber,
    AddressModel Address
);
