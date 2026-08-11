namespace BeautyPlanner.ClientService.Application.Services;

public class ClientManagementService : IClientManagementService
{
    private readonly IRepository<Client> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ClientManagementService(IRepository<Client> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ClientResult>> CreateClientAsync(CreateClientModel model)
    {
        var client = new Client(model.FirstName, model.LastName, model.Email, model.PhoneNumber, model.DateOfBirth, PrepareAddress(model.Address), model.TenantId);

        await _repository.AddAsync(client);
        await _unitOfWork.SaveChangesAsync();

        return Result<ClientResult>.Success(MapToResult(client));
    }

    public async Task<Result<ClientResult>> UpdateClientAsync(UpdateClientModel model)
    {
        var client = await _repository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("Client", model.VanityId);

        client.Update(model.FirstName, model.LastName, model.Email, model.PhoneNumber, model.DateOfBirth, PrepareAddress(model.Address));
        _repository.Update(client);
        await _unitOfWork.SaveChangesAsync();

        return Result<ClientResult>.Success(MapToResult(client));
    }

    public async Task DeleteClientAsync(Guid vanityId)
    {
        var client = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Client", vanityId);

        _repository.Delete(client);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<ClientResult>> GetClientAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Client", id);

        return Result<ClientResult>.Success(MapToResult(client));
    }

    public async Task<Result<ClientResult>> GetClientAsync(Guid vanityId)
    {
        var client = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Client", vanityId);

        return Result<ClientResult>.Success(MapToResult(client));
    }

    public async Task<Result<List<ClientResult>>> GetClientsAsync()
    {
        var clients = await _repository.ListAsync();

        return Result<List<ClientResult>>.Success(clients.Select(MapToResult).ToList());
    }

    private static Address? PrepareAddress(AddressModel? model)
    {
        return model is null ? null : new Address(model.Line1, model.Line2, model.PostalCode, model.City, model.StateProvince, model.Country);
    }

    private static ClientResult MapToResult(Client client)
    {
        var address = client.Address is null ? null : new AddressModel(client.Address.Line1, client.Address.Line2, client.Address.PostalCode,
                               client.Address.City, client.Address.StateProvince, client.Address.Country);

        return new ClientResult(
            client.Id,
            client.VanityId,
            client.FirstName,
            client.LastName,
            client.Email,
            client.PhoneNumber,
            client.DateOfBirth,
            address
        );
    }
}
