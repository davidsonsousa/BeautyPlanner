namespace BeautyPlanner.TenantService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class TenantController : BaseController
{
    private readonly ITenantService _tenantService;

    public TenantController(ITenantService tenantService, ILoggerFactory loggerFactory) : base(loggerFactory, "TenantController")
    {
        _tenantService = tenantService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTenant(CreateTenantRequest request)
    {
        LogInfo("CreateTenant called with {Name}", request.Name);

        var result = await _tenantService.CreateTenantAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("CreateTenant failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Tenant created successfully {TenantId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(GetTenant), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTenant(UpdateTenantRequest request)
    {
        LogInfo("UpdateTenant called with {Name}", request.Name);

        var result = await _tenantService.UpdateTenantAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateTenant failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Tenant updated successfully {TenantId}", result.Value!.VanityId);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTenant(Guid id)
    {
        LogInfo("GetTenant called with {id}", id);

        var result = await _tenantService.GetTenantAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetTenant failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Tenant retrieved successfully {TenantId}", result.Value!.VanityId);

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetTenants()
    {
        LogInfo("GetTenants called");

        var result = await _tenantService.GetTenantsAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetTenants failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Tenants retrieved successfully");

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTenant(Guid id)
    {
        LogInfo("DeleteTenant called with {id}", id);

        await _tenantService.DeleteTenantAsync(id);

        LogInfo("Tenant deleted successfully {id}", id);

        return NoContent();
    }
}
