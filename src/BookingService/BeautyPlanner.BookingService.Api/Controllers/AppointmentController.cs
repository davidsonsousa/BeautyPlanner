namespace BeautyPlanner.BookingService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/appointments")]
public class AppointmentController : BaseController
{
    private readonly IAppointmentManagementService _appointmentService;

    public AppointmentController(IAppointmentManagementService appointmentService, ILoggerFactory loggerFactory) : base(loggerFactory, nameof(AppointmentController))
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentRequest request)
    {
        LogInfo("CreateAppointment called for treatment {TreatmentId} and client {ClientId}", request.TreatmentId, request.ClientId);

        var result = await _appointmentService.CreateAppointmentAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("CreateAppointment failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Appointment created successfully {AppointmentId}", result.Value!.VanityId);

        return CreatedAtAction(nameof(GetAppointment), new { id = result.Value!.VanityId }, result.Value.ToResponse());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAppointment(UpdateAppointmentRequest request)
    {
        LogInfo("UpdateAppointment called for treatment {TreatmentId} and appointment {ClientId}", request.TreatmentId, request.ClientId);

        var result = await _appointmentService.UpdateAppointmentAsync(request.ToModel());

        if (!result.IsSuccess)
        {
            LogWarning("UpdateAppointment failed: {Error}", result.Error);
            return BadRequest(new { error = result.Error });
        }

        LogInfo("Appointment updated successfully {AppointmentId}", request.VanityId);

        return NoContent();
    }

    [HttpPut("{id:guid}/confirm")]
    public async Task<IActionResult> ConfirmAppointment(Guid id)
    {
        LogInfo("ConfirmAppointment called with {id}", id);

        await _appointmentService.ConfirmAppointmentAsync(id);

        LogInfo("Appointment confirmed successfully {id}", id);

        return NoContent();
    }

    [HttpPut("{id:guid}/cancel-by-client")]
    public async Task<IActionResult> CancelAppointmentByClient(Guid id)
    {
        LogInfo("CancelAppointmentByClient called with {id}", id);

        await _appointmentService.CancelAppointmentByClientAsync(id);

        LogInfo("Appointment cancelled by appointment successfully {id}", id);

        return NoContent();
    }

    [HttpPut("{id:guid}/cancel-by-staff")]
    public async Task<IActionResult> CancelAppointmentByStaff(Guid id)
    {
        LogInfo("CancelAppointmentByStaff called with {id}", id);

        await _appointmentService.CancelAppointmentByStaffAsync(id);

        LogInfo("Appointment cancelled by staff successfully {id}", id);

        return NoContent();
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<IActionResult> CompleteAppointment(Guid id)
    {
        LogInfo("CompleteAppointment called with {id}", id);

        await _appointmentService.CompleteAppointmentAsync(id);

        LogInfo("Appointment completed successfully {id}", id);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        LogInfo("GetAppointment called with {id}", id);

        var result = await _appointmentService.GetAppointmentAsync(id);

        if (!result.IsSuccess)
        {
            LogWarning("GetAppointment failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Appointment retrieved successfully {AppointmentId}", result.Value!.VanityId);

        return Ok(result.Value.ToResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        LogInfo("GetAppointments called");

        var result = await _appointmentService.GetAppointmentsAsync();

        if (!result.IsSuccess)
        {
            LogWarning("GetAppointments failed: {Error}", result.Error);
            return NotFound(new { error = result.Error });
        }

        LogInfo("Appointments retrieved successfully");

        return Ok(result.Value.ToResponse());
    }
}
