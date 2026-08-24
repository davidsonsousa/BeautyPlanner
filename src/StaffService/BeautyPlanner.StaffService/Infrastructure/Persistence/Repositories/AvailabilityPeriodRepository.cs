namespace BeautyPlanner.StaffService.Infrastructure.Persistence.Repositories;

public class AvailabilityPeriodRepository : Repository<AvailabilityPeriod>, IRepository<AvailabilityPeriod>
{
    public AvailabilityPeriodRepository(StaffDbContext dbContext) : base(dbContext)
    {

    }
}
