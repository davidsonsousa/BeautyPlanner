namespace BeautyPlanner.ClientService.Api.Contracts.Responses;

public record ClientLookupResponse(
    Guid VanityId,
    string FirstName,
    string LastName
);
