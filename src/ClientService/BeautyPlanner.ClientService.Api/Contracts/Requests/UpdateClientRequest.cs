namespace BeautyPlanner.ClientService.Api.Contracts.Requests;

public record UpdateClientRequest(
    int Id,
    Guid VanityId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime? DateOfBirth,
    AddressModel? Address,
    Guid TenantId
);
