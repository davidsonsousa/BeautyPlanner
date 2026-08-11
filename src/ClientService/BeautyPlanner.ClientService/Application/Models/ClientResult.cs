namespace BeautyPlanner.ClientService.Application.Models;

public record ClientResult(
    int Id,
    Guid VanityId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime? DateOfBirth,
    AddressModel? Address
);