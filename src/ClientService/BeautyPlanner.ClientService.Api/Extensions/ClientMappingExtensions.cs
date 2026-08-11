using Azure.Core;

namespace BeautyPlanner.ClientService.Api.Extensions;

public static class ClientMappingExtension
{
    public static CreateClientModel ToModel(this CreateClientRequest request)
    {
        return new CreateClientModel(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.DateOfBirth, request.Address, request.TenantId);
    }

    public static UpdateClientModel ToModel(this UpdateClientRequest request)
    {
        return new UpdateClientModel(request.Id, request.VanityId, request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.DateOfBirth, request.Address, request.TenantId);
    }

    public static ClientResponse ToResponse(this ClientResult? result)
    {
        return new ClientResponse(result!.Id, result.VanityId, result.FirstName, result.LastName, result.Email, result.PhoneNumber, result.DateOfBirth, result.Address);
    }

    public static List<ClientResponse> ToResponse(this List<ClientResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
