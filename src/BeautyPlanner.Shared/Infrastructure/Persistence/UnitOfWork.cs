namespace BeautyPlanner.Shared.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly BaseDbContext _dbContext;

    public UnitOfWork(BaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                await entry.ReloadAsync(cancellationToken);
            }

            throw new DbUpdateConcurrencyException("A concurrency conflict occurred. All entries were reloaded.", ex);
        }
    }
}