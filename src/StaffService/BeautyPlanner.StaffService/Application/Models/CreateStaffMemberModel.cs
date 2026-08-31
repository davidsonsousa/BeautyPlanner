namespace BeautyPlanner.StaffService.Application.Models;

public record CreateStaffMemberModel(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    Guid ProfessionId,
    DateTime? DateOfBirth,
    AddressModel? Address,
    Guid SalonId
);