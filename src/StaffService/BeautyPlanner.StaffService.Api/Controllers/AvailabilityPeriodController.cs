namespace BeautyPlanner.StaffService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/availability-periods")]
public class AvailabilityPeriodController : BaseController
{
    private IAvailabilityPeriodManagementService _availabilityPeriodService;

    public AvailabilityPeriodController(IAvailabilityPeriodManagementService availabilityPeriodService, ILoggerFactory loggerFactory) : base(loggerFactory, nameof(AvailabilityPeriodController))
    {
        _availabilityPeriodService = availabilityPeriodService;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAvailabilityPeriod(UpdateAvailabilityPeriodRequest request)
    {
        LogInfo("UpdateAvailabilityPeriod called for Staff Member {StaffMemberId}", request.StaffMemberId);

        var result = await _availabilityPeriodService.UpdateAvailabilityPeriodAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateAvailabilityPeriod failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("AvailabilityPeriod updated successfully {AvailabilityPeriodId}", result.Value!.VanityId);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAvailabilityPeriod(Guid id)
    {
        LogInfo("GetAvailabilityPeriod called with {id}", id);

        var result = await _availabilityPeriodService.GetAvailabilityPeriodAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetAvailabilityPeriod failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("AvailabilityPeriod retrieved successfully {AvailabilityPeriodId}", result.Value!.VanityId);

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailabilityPeriods()
    {
        LogInfo("GetAvailabilityPeriods called");

        var result = await _availabilityPeriodService.GetAvailabilityPeriodsAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetAvailabilityPeriods failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("AvailabilityPeriods retrieved successfully");

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAvailabilityPeriod(Guid id)
    {
        LogInfo("DeleteAvailabilityPeriod called with {id}", id);

        await _availabilityPeriodService.DeleteAvailabilityPeriodAsync(id);

        LogInfo("AvailabilityPeriod deleted successfully {id}", id);

        return NoContent();
    }
}
