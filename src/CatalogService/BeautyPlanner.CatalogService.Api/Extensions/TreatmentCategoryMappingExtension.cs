namespace BeautyPlanner.CatalogService.Api.Extensions;

public static class TreatmentCategoryMappingExtension
{
    public static CreateTreatmentCategoryModel ToModel(this CreateTreatmentCategoryRequest request)
    {
        return new CreateTreatmentCategoryModel(request.Name, request.Description);
    }

    public static UpdateTreatmentCategoryModel ToModel(this UpdateTreatmentCategoryRequest request)
    {
        return new UpdateTreatmentCategoryModel(request.Id, request.VanityId, request.Name, request.Description);
    }

    public static TreatmentCategoryResponse ToResponse(this TreatmentCategoryResult? result)
    {
        return new TreatmentCategoryResponse(result!.Id, result.VanityId, result.Name, result.Description);
    }

    public static List<TreatmentCategoryResponse> ToResponse(this List<TreatmentCategoryResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
