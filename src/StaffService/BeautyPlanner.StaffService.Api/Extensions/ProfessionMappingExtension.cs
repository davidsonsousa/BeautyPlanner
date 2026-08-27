namespace BeautyPlanner.StaffService.Api.Extensions;

public static class ProfessionMappingExtension
{
    public static CreateProfessionModel ToModel(this CreateProfessionRequest request)
    {
        return new CreateProfessionModel(request.Name, request.Description);
    }

    public static UpdateProfessionModel ToModel(this UpdateProfessionRequest request)
    {
        return new UpdateProfessionModel(request.Id, request.VanityId, request.Name, request.Description);
    }

    public static ProfessionResponse ToResponse(this ProfessionResult? result)
    {
        return new ProfessionResponse(result!.Id, result.VanityId, result.Name, result.Description);
    }

    public static List<ProfessionResponse> ToResponse(this List<ProfessionResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
