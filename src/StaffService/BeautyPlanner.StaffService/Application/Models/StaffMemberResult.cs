namespace BeautyPlanner.StaffService.Application.Models;

public record StaffMemberResult(
    int Id,
    Guid VanityId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    Guid ProfessionId,
    string ProfessionName,
    DateTime? DateOfBirth,
    AddressModel? Address,
    List<AvailabilityPeriodResult>? AvailabilityPeriods
);