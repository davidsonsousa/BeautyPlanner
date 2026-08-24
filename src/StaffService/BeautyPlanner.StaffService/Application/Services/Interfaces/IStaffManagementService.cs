namespace BeautyPlanner.StaffService.Application.Services.Interfaces;

public interface IStaffManagementService
{
    Task<Result<StaffMemberResult>> CreateStaffMemberAsync(CreateStaffMemberModel request);

    Task<Result<StaffMemberResult>> UpdateStaffMemberAsync(UpdateStaffMemberModel request);

    Task DeleteStaffMemberAsync(Guid vanityId);

    Task<Result<StaffMemberResult>> GetStaffMemberAsync(int id);

    Task<Result<StaffMemberResult>> GetStaffMemberAsync(Guid vanityId);

    Task<Result<List<StaffMemberResult>>> GetStaffMembersAsync();
}
