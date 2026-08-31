namespace BeautyPlanner.StaffService.Api.Contracts.Requests;

public record CreateStaffMemberRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    Guid ProfessionId,
    DateTime? DateOfBirth,
    AddressModel? Address,
    Guid SalonId
);
