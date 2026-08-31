namespace BeautyPlanner.StaffService.Api.Contracts.Responses;

public record ProfessionResponse(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
