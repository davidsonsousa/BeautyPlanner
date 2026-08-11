namespace BeautyPlanner.ClientService.Application.Services.Interfaces;

public interface IClientManagementService
{
    Task<Result<ClientResult>> CreateClientAsync(CreateClientModel request);

    Task<Result<ClientResult>> UpdateClientAsync(UpdateClientModel request);

    Task DeleteClientAsync(Guid vanityId);

    Task<Result<ClientResult>> GetClientAsync(int id);

    Task<Result<ClientResult>> GetClientAsync(Guid vanityId);

    Task<Result<List<ClientResult>>> GetClientsAsync();
}
