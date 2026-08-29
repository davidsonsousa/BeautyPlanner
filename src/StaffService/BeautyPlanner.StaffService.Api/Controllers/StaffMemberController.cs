namespace BeautyPlanner.StaffService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/staff-members")]
public class StaffMemberController : BaseController
{
    private IStaffManagementService _staffService;
    private IAvailabilityPeriodManagementService _availabilityPeriodService;

    public StaffMemberController(IStaffManagementService staffService, IAvailabilityPeriodManagementService availabilityPeriodService, ILoggerFactory loggerFactory) : base(loggerFactory, nameof(StaffMemberController))
    {
        _staffService = staffService;
        _availabilityPeriodService = availabilityPeriodService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStaffMember(CreateStaffMemberRequest request)
    {
        LogInfo("CreateStaffMember called with {FirstName} {LastName}", request.FirstName, request.LastName);

        var result = await _staffService.CreateStaffMemberAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("CreateStaffMember failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("StaffMember created successfully {StaffMemberId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(GetStaffMember), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStaffMember(UpdateStaffMemberRequest request)
    {
        LogInfo("UpdateStaffMember called with {FirstName} {LastName}", request.FirstName, request.LastName);

        var result = await _staffService.UpdateStaffMemberAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateStaffMember failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("StaffMember updated successfully {StaffMemberId}", result.Value!.VanityId);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStaffMember(Guid id)
    {
        LogInfo("GetStaffMember called with {id}", id);

        var result = await _staffService.GetStaffMemberAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetStaffMember failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("StaffMember retrieved successfully {StaffMemberId}", result.Value!.VanityId);

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetStaffMembers()
    {
        LogInfo("GetStaffMembers called");

        var result = await _staffService.GetStaffMembersAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetStaffMembers failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("StaffMembers retrieved successfully");

        return Ok(result.Value.ToResponse());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStaffMember(Guid id)
    {
        LogInfo("DeleteStaffMember called with {id}", id);

        await _staffService.DeleteStaffMemberAsync(id);

        LogInfo("StaffMember deleted successfully {id}", id);

        return NoContent();
    }

    [HttpPost("{staffMemberId}/availability-periods")]
    public async Task<IActionResult> CreateAvailabilityPeriod(Guid staffMemberId, CreateAvailabilityPeriodRequest request)
    {
        LogInfo("CreateAvailabilityPeriod called for Staff Member {staffMemberId}", staffMemberId);

        var result = await _availabilityPeriodService.CreateAvailabilityPeriodAsync(request.ToModel(staffMemberId));

        if (!result.IsSuccess)
        {
            LogWarning("CreateAvailabilityPeriod failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("AvailabilityPeriod created successfully {AvailabilityPeriodId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(CreateAvailabilityPeriod), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }


    [HttpGet("{staffMemberId}/availability-periods")]
    public async Task<IActionResult> GetAvailabilityPeriods(Guid staffMemberId)
    {
        LogInfo("GetAvailabilityPeriods called with Staff Member {id}", staffMemberId);

        var result = await _availabilityPeriodService.GetAvailabilityPeriodsForStaffMemberAsync(staffMemberId);

        if (!result.IsSuccess)
        {
            LogWarning("GetAvailabilityPeriods failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("GetAvailabilityPeriods retrieved successfully for Staff Member {StaffMemberId}", staffMemberId);

        return Ok(result.Value.ToResponse());
    }
}
