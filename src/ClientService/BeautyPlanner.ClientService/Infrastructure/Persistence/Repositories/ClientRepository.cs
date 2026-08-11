namespace BeautyPlanner.ClientService.Infrastructure.Persistence.Repositories;

public class ClientRepository : Repository<Client>, IRepository<Client>
{
    public ClientRepository(ClientDbContext dbContext) : base(dbContext)
    {

    }
}
