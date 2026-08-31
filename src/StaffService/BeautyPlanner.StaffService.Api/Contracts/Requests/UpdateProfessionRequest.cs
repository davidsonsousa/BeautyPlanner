namespace BeautyPlanner.StaffService.Api.Contracts.Requests;

public record UpdateProfessionRequest(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
