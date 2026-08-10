namespace BeautyPlanner.ClientService.Infrastructure.Persistence;

public class ClientDbContext : BaseDbContext
{
    public DbSet<Client> Tenants => Set<Client>();

    public ClientDbContext(DbContextOptions<ClientDbContext> options, IUserContext uc) : base(options, uc)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Model configuration
        builder.ApplyConfigurationsFromAssembly(typeof(ClientDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
