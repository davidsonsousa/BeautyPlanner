namespace BeautyPlanner.TenantService.Application.Interfaces;

public interface IReadOnlyRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Get record by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Gets record by vanity id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByVanityIdAsync(Guid id);

    /// <summary>
    /// List records
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? filter = null);

    /// <summary>
    /// Count items
    /// </summary>
    /// <returns></returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
}
