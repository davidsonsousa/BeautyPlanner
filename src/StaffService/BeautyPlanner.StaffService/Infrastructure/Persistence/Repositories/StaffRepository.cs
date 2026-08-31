namespace BeautyPlanner.StaffService.Infrastructure.Persistence.Repositories;

public class StaffRepository : Repository<StaffMember>, IStaffRepository
{
    public StaffRepository(StaffDbContext dbContext) : base(dbContext)
    {

    }

    public async override Task<StaffMember?> GetByIdAsync(int id)
    {
        return await GetValidRecords().Where(x => x.Id == id).Include(sm => sm.Profession).FirstOrDefaultAsync();
    }

    public async override Task<StaffMember?> GetByVanityIdAsync(Guid id)
    {
        return await GetValidRecords().Where(x => x.VanityId == id).Include(sm => sm.Profession).FirstOrDefaultAsync();
    }

    public async Task<List<StaffMember>> ListWithProfessionAsync(Expression<Func<StaffMember, bool>>? filter = null)
    {
        var records = GetValidRecords();

        if (filter is not null)
        {
            records = records.Where(filter);
        }

        return await records.Include(sm => sm.Profession).AsNoTracking().ToListAsync();
    }
}
