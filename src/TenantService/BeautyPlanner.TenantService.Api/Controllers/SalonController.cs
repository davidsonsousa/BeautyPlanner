namespace BeautyPlanner.TenantService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class SalonController : BaseController
{
    private ISalonManagementService _salonService;

    public SalonController(ISalonManagementService salonService, ILoggerFactory loggerFactory) : base(loggerFactory, nameof(SalonController))
    {
        _salonService = salonService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSalon(CreateSalonRequest request)
    {
        LogInfo("CreateSalon called with {Name}", request.Name);

        var result = await _salonService.CreateSalonAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("CreateSalon failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Salon created successfully {SalonId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(GetSalon), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSalon(UpdateSalonRequest request)
    {
        LogInfo("UpdateSalon called with {Name}", request.Name);

        var result = await _salonService.UpdateSalonAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateSalon failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Salon updated successfully {SalonId}", result.Value!.VanityId);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSalon(Guid id)
    {
        LogInfo("GetSalon called with {id}", id);

        var result = await _salonService.GetSalonAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetSalon failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Salon retrieved successfully {SalonId}", result.Value!.VanityId);

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetSalons()
    {
        LogInfo("GetSalons called");

        var result = await _salonService.GetSalonsAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetSalons failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Salons retrieved successfully");

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSalon(Guid id)
    {
        LogInfo("DeleteSalon called with {id}", id);

        await _salonService.DeleteSalonAsync(id);

        LogInfo("Salon deleted successfully {id}", id);

        return NoContent();
    }
}
