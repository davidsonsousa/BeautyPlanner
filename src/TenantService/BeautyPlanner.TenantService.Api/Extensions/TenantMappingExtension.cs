namespace BeautyPlanner.TenantService.Api.Extensions;

public static class TenantMappingExtension
{
    public static CreateTenantModel ToModel(this CreateTenantRequest request)
    {
        return new CreateTenantModel(request.Name, request.Description);
    }

    public static UpdateTenantModel ToModel(this UpdateTenantRequest request)
    {
        return new UpdateTenantModel(request.Id, request.VanityId, request.Name, request.Description);
    }

    public static TenantResponse ToResponse(this TenantResult? result)
    {
        return new TenantResponse(result!.Id, result.VanityId, result.Name, result.Description);
    }

    public static List<TenantResponse> ToResponse(this List<TenantResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
