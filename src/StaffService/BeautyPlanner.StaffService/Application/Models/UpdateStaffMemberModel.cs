namespace BeautyPlanner.StaffService.Application.Models;

public record UpdateStaffMemberModel(
    int Id,
    Guid VanityId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    Guid ProfessionId,
    DateTime? DateOfBirth,
    AddressModel? Address,
    Guid TenantId
);