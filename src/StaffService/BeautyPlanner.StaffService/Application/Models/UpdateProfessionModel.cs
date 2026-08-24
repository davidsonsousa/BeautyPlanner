namespace BeautyPlanner.StaffService.Application.Models;

public record UpdateProfessionModel(
    int Id,
    Guid VanityId,
    string Name,
    string? Description
);
