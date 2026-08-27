namespace BeautyPlanner.StaffService.Api.Contracts.Responses;

public record StaffMemberResponse(
    int Id,
    Guid VanityId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string ProfessionName,
    DateTime? DateOfBirth,
    AddressModel? Address
);
