namespace BeautyPlanner.TenantService.Application.Models;

public record CreateSalonModel(
    string Name,
    string? Description,
    string Email,
    string PhoneNumber,
    AddressModel Address,
    Guid TenantId
);
