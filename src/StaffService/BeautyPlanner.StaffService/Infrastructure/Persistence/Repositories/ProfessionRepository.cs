namespace BeautyPlanner.StaffService.Infrastructure.Persistence.Repositories;

public class ProfessionRepository : Repository<Profession>, IRepository<Profession>
{
    public ProfessionRepository(StaffDbContext dbContext) : base(dbContext)
    {

    }
}
