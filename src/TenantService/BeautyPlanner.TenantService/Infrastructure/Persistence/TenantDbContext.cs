namespace BeautyPlanner.TenantService.Infrastructure.Persistence;

public class TenantDbContext : BaseDbContext
{
    public DbSet<Tenant> Tenants => Set<Tenant>();

    public DbSet<Salon> Salons => Set<Salon>();

    public TenantDbContext(DbContextOptions<TenantDbContext> options, IUserContext uc) : base(options, uc)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Model configuration
        builder.ApplyConfigurationsFromAssembly(typeof(TenantDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
