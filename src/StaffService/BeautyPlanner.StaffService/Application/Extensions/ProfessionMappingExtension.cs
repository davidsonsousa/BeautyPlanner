namespace BeautyPlanner.StaffService.Application.Extensions;

public static class ProfessionMappingExtension
{
    public static ProfessionResult ToResult(this Profession profession)
    {
        return new ProfessionResult(
            profession.Id,
            profession.VanityId,
            profession.Name,
            profession.Description
        );

    }

    public static List<ProfessionResult> ToResult(this ICollection<Profession> profession)
    {
        return profession.Select(ToResult).ToList();
    }
}
