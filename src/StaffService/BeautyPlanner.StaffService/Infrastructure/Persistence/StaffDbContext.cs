namespace BeautyPlanner.StaffService.Infrastructure.Persistence;

public class StaffDbContext : BaseDbContext
{
    public DbSet<StaffMember> StaffMembers => Set<StaffMember>();

    public DbSet<Profession> Professions => Set<Profession>();

    public DbSet<AvailabilityPeriod> AvailabilityPeriods => Set<AvailabilityPeriod>();

    public StaffDbContext(DbContextOptions<StaffDbContext> options, IUserContext uc) : base(options, uc)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Model configuration
        builder.ApplyConfigurationsFromAssembly(typeof(StaffDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
