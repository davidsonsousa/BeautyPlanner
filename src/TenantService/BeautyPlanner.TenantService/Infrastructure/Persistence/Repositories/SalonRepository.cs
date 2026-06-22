namespace BeautyPlanner.TenantService.Infrastructure.Persistence.Repositories;

public class SalonRepository : Repository<Salon>, IRepository<Salon>
{
    public SalonRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    //public override Task<Salon?> GetByVanityIdAsync(Guid id)
    //{
    //    return GetValidRecords().Include(s => s.Address).Where(x => x.VanityId == id).FirstOrDefaultAsync();
    //}
}
