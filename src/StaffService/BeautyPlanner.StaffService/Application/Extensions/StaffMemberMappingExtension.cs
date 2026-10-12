namespace BeautyPlanner.StaffService.Application.Extensions;

public static class StaffMemberMappingExtension
{
    public static StaffMemberResult ToResult(this StaffMember staffMember)
    {
        var address = new AddressModel(staffMember.Address.Line1, staffMember.Address.Line2, staffMember.Address.PostalCode,
                               staffMember.Address.City, staffMember.Address.StateProvince, staffMember.Address.Country);

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
            address,
            staffMember.AvailabilityPeriods.ToResult()
        );
    }

    public static List<StaffMemberResult> ToResult(this ICollection<StaffMember> staffMember)
    {
        return staffMember.Select(ToResult).ToList();
    }
}
