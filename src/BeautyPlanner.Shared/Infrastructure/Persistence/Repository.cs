namespace BeautyPlanner.Shared.Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity>, IReadOnlyRepository<TEntity> where TEntity : AuditableEntity
{
    protected readonly BaseDbContext _dbContext;

    public Repository(BaseDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await GetValidRecords().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity?> GetByVanityIdAsync(Guid id)
    {
        return await GetValidRecords().Where(x => x.VanityId == id).FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        var records = GetValidRecords();

        if (filter is not null)
        {
            records = records.Where(filter);
        }

        return await records.AsNoTracking().ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        var records = GetValidRecords();

        if (filter is not null)
        {
            records = records.Where(filter);
        }

        return await records.CountAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _dbContext.AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        // Soft delete
        _dbContext.Entry(entity).State = EntityState.Modified;
        entity.IsDeleted = true;
    }

    public void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbContext.Update(entity);
    }

    protected IQueryable<TEntity> GetValidRecords()
    {
        return _dbContext.Set<TEntity>().Where(q => q.IsDeleted == false);
    }
}
