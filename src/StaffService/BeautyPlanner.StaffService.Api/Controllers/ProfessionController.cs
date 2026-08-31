namespace BeautyPlanner.StaffService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class ProfessionController : BaseController
{
    private IProfessionManagementService _professionService;

    public ProfessionController(IProfessionManagementService professionService, ILoggerFactory loggerFactory) : base(loggerFactory, nameof(ProfessionController))
    {
        _professionService = professionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfession(CreateProfessionRequest request)
    {
        LogInfo("CreateProfession called with {Name}", request.Name);

        var result = await _professionService.CreateProfessionAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("CreateProfession failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Profession created successfully {ProfessionId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(GetProfession), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfession(UpdateProfessionRequest request)
    {
        LogInfo("UpdateProfession called with {Name}", request.Name);

        var result = await _professionService.UpdateProfessionAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateProfession failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Profession updated successfully {ProfessionId}", result.Value!.VanityId);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProfession(Guid id)
    {
        LogInfo("GetProfession called with {id}", id);

        var result = await _professionService.GetProfessionAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetProfession failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Profession retrieved successfully {ProfessionId}", result.Value!.VanityId);

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetProfessions()
    {
        LogInfo("GetProfessions called");

        var result = await _professionService.GetProfessionsAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetProfessions failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Professions retrieved successfully");

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProfession(Guid id)
    {
        LogInfo("DeleteProfession called with {id}", id);

        await _professionService.DeleteProfessionAsync(id);

        LogInfo("Profession deleted successfully {id}", id);

        return NoContent();
    }
}
