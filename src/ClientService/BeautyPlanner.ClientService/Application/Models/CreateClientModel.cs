namespace BeautyPlanner.ClientService.Application.Models;

public record CreateClientModel(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime? DateOfBirth,
    AddressModel? Address,
    Guid TenantId
);