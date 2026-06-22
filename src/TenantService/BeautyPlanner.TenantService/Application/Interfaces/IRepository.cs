namespace BeautyPlanner.TenantService.Application.Interfaces;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Inserts a record
    /// </summary>
    /// <param name="entity"></param>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Updates record
    /// </summary>
    /// <param name="entity"></param>
    void Update(TEntity entity);

    /// <summary>
    /// Deletes record from database
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);
}
