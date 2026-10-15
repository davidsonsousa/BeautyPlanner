namespace BeautyPlanner.CatalogService.Api.Contracts.Responses;

public record TreatmentLookupResponse(
    Guid VanityId,
    string Name,
    int DurationInMinutes,
    Guid ProfessionId,
    Guid SalonId
);
