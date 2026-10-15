namespace BeautyPlanner.StaffService.Api.Contracts.Responses;

public record StaffMemberLookupResponse(
    Guid VanityId,
    string FirstName,
    string LastName,
    Guid ProfessionId,
    List<AvailabilityPeriodResponse> AvailabilityPeriods
);
