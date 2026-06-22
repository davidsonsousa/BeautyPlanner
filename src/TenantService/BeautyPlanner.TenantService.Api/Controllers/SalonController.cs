namespace BeautyPlanner.TenantService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SalonController : ControllerBase
{
    private ISalonService _salonService;

    public SalonController(ISalonService salonService)
    {
        _salonService = salonService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSalon(CreateSalonRequest request)
    {
        var result = await _salonService.CreateSalonAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.Error });
        }

        return CreatedAtAction(nameof(GetSalon), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSalon(UpdateSalonRequest request)
    {
        var result = await _salonService.UpdateSalonAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.Error });
        }

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSalon(Guid id)
    {
        var result = await _salonService.GetSalonAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetSalons()
    {
        var result = await _salonService.GetSalonsAsync();

        if (!result.IsSuccess)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSalon(Guid id)
    {
        await _salonService.DeleteSalonAsync(id);

        return NoContent();
    }
}
