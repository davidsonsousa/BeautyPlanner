namespace BeautyPlanner.BookingService.Application.Services.Interfaces;

public interface IAppointmentManagementService
{
    Task<Result<AppointmentResult>> CreateAppointmentAsync(CreateAppointmentModel request);

    Task<Result> UpdateAppointmentAsync(UpdateAppointmentModel request);

    Task ConfirmAppointmentAsync(Guid vanityId);

    Task CancelAppointmentByClientAsync(Guid vanityId);

    Task CancelAppointmentByStaffAsync (Guid vanityId);

    Task CompleteAppointmentAsync(Guid vanityId);

    Task<Result<AppointmentResult>> GetAppointmentAsync(int id);

    Task<Result<AppointmentResult>> GetAppointmentAsync(Guid vanityId);

    Task<Result<List<AppointmentResult>>> GetAppointmentsAsync();
}
