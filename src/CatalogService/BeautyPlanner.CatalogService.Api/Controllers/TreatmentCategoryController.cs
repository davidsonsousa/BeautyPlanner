namespace BeautyPlanner.CatalogService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/treatment-categories")]
public class TreatmentCategoryController : BaseController
{
    private readonly ITreatmentCategoryManagementService _treatmentCategoryService;

    public TreatmentCategoryController(ITreatmentCategoryManagementService treatmentCategoryService, ILoggerFactory loggerFactory) : base(loggerFactory, nameof(TreatmentCategoryController))
    {
        _treatmentCategoryService = treatmentCategoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTreatmentCategory(CreateTreatmentCategoryRequest request)
    {
        LogInfo("CreateTreatmentCategory called with {Name}", request.Name);

        var result = await _treatmentCategoryService.CreateTreatmentCategoryAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("CreateTreatmentCategory failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("TreatmentCategory created successfully {TreatmentCategoryId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(GetTreatmentCategory), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTreatmentCategory(UpdateTreatmentCategoryRequest request)
    {
        LogInfo("UpdateTreatmentCategory called with {Name}", request.Name);

        var result = await _treatmentCategoryService.UpdateTreatmentCategoryAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateTreatmentCategory failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("TreatmentCategory updated successfully {TreatmentCategoryId}", result.Value!.VanityId);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTreatmentCategory(Guid id)
    {
        LogInfo("GetTreatmentCategory called with {id}", id);

        var result = await _treatmentCategoryService.GetTreatmentCategoryAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetTreatmentCategory failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("TreatmentCategory retrieved successfully {TreatmentCategoryId}", result.Value!.VanityId);

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetTreatmentCategorys()
    {
        LogInfo("GetTreatmentCategorys called");

        var result = await _treatmentCategoryService.GetTreatmentCategorysAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetTreatmentCategorys failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("TreatmentCategorys retrieved successfully");

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTreatmentCategory(Guid id)
    {
        LogInfo("DeleteTreatmentCategory called with {id}", id);

        await _treatmentCategoryService.DeleteTreatmentCategoryAsync(id);

        LogInfo("TreatmentCategory deleted successfully {id}", id);

        return NoContent();
    }
}
