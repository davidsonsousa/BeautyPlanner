namespace BeautyPlanner.CatalogService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/treatments")]
public class TreatmentController : BaseController
{
    private ITreatmentManagementService _salonService;

    public TreatmentController(ITreatmentManagementService salonService, ILoggerFactory loggerFactory) : base(loggerFactory, nameof(TreatmentController))
    {
        _salonService = salonService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTreatment(CreateTreatmentRequest request)
    {
        LogInfo("CreateTreatment called with {Name}", request.Name);

        var result = await _salonService.CreateTreatmentAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("CreateTreatment failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Treatment created successfully {TreatmentId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(GetTreatment), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTreatment(UpdateTreatmentRequest request)
    {
        LogInfo("UpdateTreatment called with {Name}", request.Name);

        var result = await _salonService.UpdateTreatmentAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateTreatment failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Treatment updated successfully {TreatmentId}", result.Value!.VanityId);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTreatment(Guid id)
    {
        LogInfo("GetTreatment called with {id}", id);

        var result = await _salonService.GetTreatmentAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetTreatment failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Treatment retrieved successfully {TreatmentId}", result.Value!.VanityId);

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetTreatments()
    {
        LogInfo("GetTreatments called");

        var result = await _salonService.GetTreatmentsAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetTreatments failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Treatments retrieved successfully");

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTreatment(Guid id)
    {
        LogInfo("DeleteTreatment called with {id}", id);

        await _salonService.DeleteTreatmentAsync(id);

        LogInfo("Treatment deleted successfully {id}", id);

        return NoContent();
    }
}
