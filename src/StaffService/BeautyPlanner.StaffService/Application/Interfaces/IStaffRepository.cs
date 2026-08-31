namespace BeautyPlanner.StaffService.Application.Interfaces;

public interface IStaffRepository : IRepository<StaffMember>
{
    Task<List<StaffMember>> ListWithProfessionAsync(Expression<Func<StaffMember, bool>>? filter = null);
}
