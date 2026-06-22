namespace BeautyPlanner.TenantService.Api.Extensions;

public static class SalonMappingExtension
{
    public static CreateSalonModel ToModel(this CreateSalonRequest request)
    {
        return new CreateSalonModel(request.Name, request.Description, request.Email, request.PhoneNumber, request.Address, request.TenantId);
    }

    public static UpdateSalonModel ToModel(this UpdateSalonRequest request)
    {
        return new UpdateSalonModel(request.Id, request.VanityId, request.Name, request.Description, request.Email, request.PhoneNumber, request.Address, request.TenantId);
    }

    public static SalonResponse ToResponse(this SalonResult? result)
    {
        return new SalonResponse(result!.Id, result.VanityId, result.Name, result.Description, result.Email, result.PhoneNumber, result.Address);
    }

    public static List<SalonResponse> ToResponse(this List<SalonResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
