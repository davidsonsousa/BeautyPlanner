namespace BeautyPlanner.StaffService.Application.Models;

public record StaffMemberResult(
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