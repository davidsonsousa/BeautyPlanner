namespace BeautyPlanner.StaffService.Infrastructure.Persistence.Repositories;

public class StaffRepository : Repository<StaffMember>, IRepository<StaffMember>
{
    public StaffRepository(StaffDbContext dbContext) : base(dbContext)
    {

    }
}
