namespace BeautyPlanner.BookingService.Infrastructure.Persistence;

public class BookingDbContext : BaseDbContext
{
    public DbSet<Appointment> Appointments => Set<Appointment>();

    public BookingDbContext(DbContextOptions<BookingDbContext> options, IUserContext uc) : base(options, uc)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Model configuration
        builder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
