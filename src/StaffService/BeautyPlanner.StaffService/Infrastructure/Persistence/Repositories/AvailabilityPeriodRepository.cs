namespace BeautyPlanner.StaffService.Infrastructure.Persistence.Repositories;

public class AvailabilityPeriodRepository : Repository<AvailabilityPeriod>, IAvailabilityPeriodRepository
{
    public AvailabilityPeriodRepository(StaffDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<AvailabilityPeriod>> ListForStaffMember(int staffMemberId)
    {
        return await GetValidRecords().Where(x => x.StaffMemberId == staffMemberId).ToListAsync();
    }

    public async Task<List<AvailabilityPeriod>> ListForStaffMember(Guid staffMemberVanityId)
    {
        return await GetValidRecords().Include(ap => ap.StaffMember).Where(x => x.StaffMember.VanityId == staffMemberVanityId).ToListAsync();
    }
}
