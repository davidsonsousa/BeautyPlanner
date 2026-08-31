namespace BeautyPlanner.StaffService.Api.Extensions;

public static class StaffMemberMappingExtension
{
    public static CreateStaffMemberModel ToModel(this CreateStaffMemberRequest request)
    {
        return new CreateStaffMemberModel(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.ProfessionId,
                                          request.DateOfBirth, request.Address, request.SalonId);
    }

    public static UpdateStaffMemberModel ToModel(this UpdateStaffMemberRequest request)
    {
        return new UpdateStaffMemberModel(request.Id, request.VanityId, request.FirstName, request.LastName, request.Email, request.PhoneNumber,
                                          request.ProfessionId, request.DateOfBirth, request.Address, request.SalonId);
    }

    public static StaffMemberResponse ToResponse(this StaffMemberResult? result)
    {
        return new StaffMemberResponse(result!.Id, result.VanityId, result.FirstName, result.LastName, result.Email, result.PhoneNumber,
                                       result.ProfessionName, result.DateOfBirth, result.Address);
    }

    public static List<StaffMemberResponse> ToResponse(this List<StaffMemberResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
