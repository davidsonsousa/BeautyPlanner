namespace BeautyPlanner.StaffService.Application.Models;

public record ProfessionResult(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
