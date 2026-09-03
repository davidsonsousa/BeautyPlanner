namespace BeautyPlanner.CatalogService.Infrastructure.Persistence;

public class CatalogDbContext : BaseDbContext
{
    public DbSet<TreatmentCategory> TreatmentCategories => Set<TreatmentCategory>();

    public DbSet<Treatment> Treatments => Set<Treatment>();

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IUserContext uc) : base(options, uc)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Model configuration
        builder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
