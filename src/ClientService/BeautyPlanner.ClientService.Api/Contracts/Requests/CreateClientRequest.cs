namespace BeautyPlanner.ClientService.Api.Contracts.Requests;

public record CreateClientRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime? DateOfBirth,
    AddressModel? Address,
    Guid TenantId
);
