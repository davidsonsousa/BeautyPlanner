namespace BeautyPlanner.CatalogService.Infrastructure.Persistence.Repositories;

public class TreatmentRepository : Repository<Treatment>, ITreatmentRepository
{
    public TreatmentRepository(CatalogDbContext dbContext) : base(dbContext)
    {

    }

    public async override Task<Treatment?> GetByIdAsync(int id)
    {
        return await GetValidRecords().Where(x => x.Id == id).Include(t => t.TreatmentCategory).FirstOrDefaultAsync();
    }

    public async override Task<Treatment?> GetByVanityIdAsync(Guid id)
    {
        return await GetValidRecords().Where(x => x.VanityId == id).Include(t => t.TreatmentCategory).FirstOrDefaultAsync();
    }

    public async Task<List<Treatment>> ListWithTreatmentCategoriesAsync(Expression<Func<Treatment, bool>>? filter = null)
    {
        var records = GetValidRecords();

        if (filter is not null)
        {
            records = records.Where(filter);
        }

        return await records.Include(t => t.TreatmentCategory).AsNoTracking().ToListAsync();
    }
}
