namespace BeautyPlanner.Shared.Infrastructure.Persistence;

public abstract class BaseDbContext : DbContext
{
    protected readonly IUserContext userContext;

    public BaseDbContext(DbContextOptions options, IUserContext uc) : base(options)
    {
        userContext = uc;
        //Database.SetInitializer(new BeautyPlannerInitializer());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        var currentUsername = userContext.GetCurrentUsername();
        var timeStamp = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = timeStamp;
                entry.Entity.CreatedBy = currentUsername;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Entity.IsDeleted)
                {
                    entry.Entity.DeletedAt = timeStamp;
                    entry.Entity.DeletedBy = currentUsername;
                }
                else
                {
                    entry.Entity.UpdatedAt = timeStamp;
                    entry.Entity.UpdatedBy = currentUsername;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
