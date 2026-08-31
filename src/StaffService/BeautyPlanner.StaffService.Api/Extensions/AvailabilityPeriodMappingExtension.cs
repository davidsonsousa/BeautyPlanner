namespace BeautyPlanner.StaffService.Api.Extensions;

public static class AvailabilityPeriodMappingExtension
{
    public static CreateAvailabilityPeriodModel ToModel(this CreateAvailabilityPeriodRequest request, Guid staffMemberId)
    {
        return new CreateAvailabilityPeriodModel(request.DayOfWeek, request.StartTime, request.EndTime, staffMemberId);
    }

    public static UpdateAvailabilityPeriodModel ToModel(this UpdateAvailabilityPeriodRequest request)
    {
        return new UpdateAvailabilityPeriodModel(request.VanityId, request.DayOfWeek, request.StartTime, request.EndTime, request.StaffMemberId);
    }

    public static AvailabilityPeriodResponse ToResponse(this AvailabilityPeriodResult? result)
    {
        return new AvailabilityPeriodResponse(result!.Id, result.VanityId, result.DayOfWeek, result.StartTime, result.EndTime);
    }

    public static List<AvailabilityPeriodResponse> ToResponse(this List<AvailabilityPeriodResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
