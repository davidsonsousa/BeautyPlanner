namespace BeautyPlanner.StaffService.Application.Extensions;

public static class StaffMemberMappingExtension
{
    public static StaffMemberResult ToResult(this StaffMember staffMember)
    {
        return new StaffMemberResult(
            staffMember.Id,
            staffMember.VanityId,
            staffMember.FirstName,
            staffMember.LastName,
            staffMember.Email,
            staffMember.PhoneNumber,
            staffMember.Profession.VanityId,
            staffMember.Profession.Name,
            staffMember.DateOfBirth,
            staffMember.Address.ToModel(),
            staffMember.AvailabilityPeriods.ToResult()
        );
    }

    public static List<StaffMemberResult> ToResult(this ICollection<StaffMember> staffMember)
    {
        return staffMember.Select(ToResult).ToList();
    }
}
