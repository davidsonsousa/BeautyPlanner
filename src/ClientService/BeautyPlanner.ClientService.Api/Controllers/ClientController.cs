namespace BeautyPlanner.ClientService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/clients")]
public class ClientController : BaseController
{
    IClientManagementService _clientService;

    public ClientController(IClientManagementService clientService, ILoggerFactory loggerFactory) : base(loggerFactory, nameof(ClientController))
    {
        _clientService = clientService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(CreateClientRequest request)
    {
        LogInfo("CreateClient called with {FirstName} {LastName}", request.FirstName, request.LastName);

        var result = await _clientService.CreateClientAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("CreateClient failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Client created successfully {ClientId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(GetClient), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateClient(UpdateClientRequest request)
    {
        LogInfo("UpdateClient called with {FirstName} {LastName}", request.FirstName, request.LastName);

        var result = await _clientService.UpdateClientAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateClient failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Client updated successfully {ClientId}", request.VanityId);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetClient(Guid id)
    {
        return await GetClientInternal(id, client => client.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        LogInfo("GetClients called");

        var result = await _clientService.GetClientsAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetClients failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Clients retrieved successfully");

        return Ok(result.Value.ToResponse());
    }

    [HttpGet("{id:guid}/lookup")]
    public async Task<IActionResult> GetClientLookup(Guid id)
    {
        return await GetClientInternal(id, client => client.ToLookupResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        LogInfo("DeleteClient called with {id}", id);

        await _clientService.DeleteClientAsync(id);

        LogInfo("Client deleted successfully {id}", id);

        return NoContent();
    }

    private async Task<IActionResult> GetClientInternal<TResponse>(Guid id, Func<ClientResult, TResponse> mapper)
    {
        LogInfo("GetClient called with {id}", id);

        var result = await _clientService.GetClientAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetClient failed: {Error}", result.Error);

            return NotFound(new { error = result.Error });
        }

        LogInfo("Client retrieved successfully {ClientId}", result.Value!.VanityId);

        return Ok(mapper(result.Value));
    }
}
