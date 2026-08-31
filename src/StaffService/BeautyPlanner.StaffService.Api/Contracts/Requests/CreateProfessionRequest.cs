namespace BeautyPlanner.StaffService.Api.Contracts.Requests;

public record CreateProfessionRequest(
    string Name,
    string? Description
);
