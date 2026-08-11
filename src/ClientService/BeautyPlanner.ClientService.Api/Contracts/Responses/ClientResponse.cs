namespace BeautyPlanner.ClientService.Api.Contracts.Responses;

public record ClientResponse(
    int Id,
    Guid VanityId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime? DateOfBirth,
    AddressModel? Address
);
