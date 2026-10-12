namespace BeautyPlanner.StaffService.Application.Extensions;

public static class AvailabilityPeriodMappingExtension
{
    public static AvailabilityPeriodResult ToResult(this AvailabilityPeriod availabilityPeriod)
    {
        return new AvailabilityPeriodResult(
            availabilityPeriod.Id,
            availabilityPeriod.VanityId,
            availabilityPeriod.DayOfWeek,
            availabilityPeriod.StartTime,
            availabilityPeriod.EndTime);
    }

    public static List<AvailabilityPeriodResult> ToResult(this ICollection<AvailabilityPeriod> availabilityPeriod)
    {
        return availabilityPeriod is null ? [] : availabilityPeriod.Select(ToResult).ToList();
    }
}
