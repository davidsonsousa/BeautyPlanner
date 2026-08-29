namespace BeautyPlanner.StaffService.Application.Interfaces;

public interface IAvailabilityPeriodRepository : IRepository<AvailabilityPeriod>
{
    Task<List<AvailabilityPeriod>> ListForStaffMember(int staffMemberId);

    Task<List<AvailabilityPeriod>> ListForStaffMember(Guid staffMemberVanityId);
}
