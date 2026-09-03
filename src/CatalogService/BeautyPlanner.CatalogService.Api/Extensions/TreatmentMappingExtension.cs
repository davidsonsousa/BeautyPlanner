namespace BeautyPlanner.CatalogService.Api.Extensions;

public static class TreatmentMappingExtension
{
    public static CreateTreatmentModel ToModel(this CreateTreatmentRequest request)
    {
        return new CreateTreatmentModel(request.Name, request.Description, request.Price, request.DurationInMinutes, request.SalonId,
                                        request.ProfessionId, request.TreatmentCategoryId);
    }

    public static UpdateTreatmentModel ToModel(this UpdateTreatmentRequest request)
    {
        return new UpdateTreatmentModel(request.Id, request.VanityId, request.Name, request.Description, request.Price, request.DurationInMinutes,
                                        request.ProfessionId, request.TreatmentCategoryId);
    }

    public static TreatmentResponse ToResponse(this TreatmentResult? result)
    {
        return new TreatmentResponse(result!.Id, result.VanityId, result.Name, result.Description, result.Price, result.DurationInMinutes,
                                     result.ProfessionId, result.TreatmentCategory);
    }

    public static List<TreatmentResponse> ToResponse(this List<TreatmentResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
