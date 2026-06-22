namespace BeautyPlanner.TenantService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TenantController : ControllerBase
{
    private ITenantService _tenantService;

    public TenantController(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTenant(CreateTenantRequest request)
    {
        var result = await _tenantService.CreateTenantAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.Error });
        }

        return CreatedAtAction(nameof(GetTenant), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTenant(UpdateTenantRequest request)
    {
        var result = await _tenantService.UpdateTenantAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.Error });
        }

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTenant(Guid id)
    {
        var result = await _tenantService.GetTenantAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetTenants()
    {
        var result = await _tenantService.GetTenantsAsync();

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTenant(Guid id)
    {
        await _tenantService.DeleteTenantAsync(id);

        return NoContent();
    }
}
