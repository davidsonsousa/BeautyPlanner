namespace BeautyPlanner.BookingService.Application.Services;

public class AppointmentManagementService : IAppointmentManagementService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentManagementService(IAppointmentRepository repository, IUnitOfWork unitOfWork)
    {
        _appointmentRepository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AppointmentResult>> CreateAppointmentAsync(CreateAppointmentModel model)
    {
        var appointment = new Appointment(model.Period, model.ClientId, model.StaffMemberId, model.TreatmentId, model.SalonId);

        await _appointmentRepository.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        return Result<AppointmentResult>.Success(MapToResult(appointment));
    }

    public async Task<Result> UpdateAppointmentAsync(UpdateAppointmentModel model)
    {
        var appointment = await GetAppointmentOrThrowAsync(model.VanityId);

        appointment.Update(model.Period, model.ClientId, model.StaffMemberId, model.TreatmentId);
        _appointmentRepository.Update(appointment);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task ConfirmAppointmentAsync(Guid vanityId)
    {
        var appointment = await GetAppointmentOrThrowAsync(vanityId);

        appointment.Confirm();
        _appointmentRepository.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelAppointmentByClientAsync(Guid vanityId)
    {
        var appointment = await GetAppointmentOrThrowAsync(vanityId);

        appointment.ClientCancel();
        _appointmentRepository.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CancelAppointmentByStaffAsync(Guid vanityId)
    {
        var appointment = await GetAppointmentOrThrowAsync(vanityId);

        appointment.StaffCancel();
        _appointmentRepository.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CompleteAppointmentAsync(Guid vanityId)
    {
        var appointment = await GetAppointmentOrThrowAsync(vanityId);

        appointment.Complete();
        _appointmentRepository.Update(appointment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<AppointmentResult>> GetAppointmentAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id) ?? throw new NotFoundException("Appointment", id);

        return Result<AppointmentResult>.Success(MapToResult(appointment));
    }

    public async Task<Result<AppointmentResult>> GetAppointmentAsync(Guid vanityId)
    {
        var appointment = await GetAppointmentOrThrowAsync(vanityId);

        return Result<AppointmentResult>.Success(MapToResult(appointment));
    }

    public async Task<Result<List<AppointmentResult>>> GetAppointmentsAsync()
    {
        var appointment = await _appointmentRepository.ListAsync();

        return Result<List<AppointmentResult>>.Success(appointment.Select(MapToResult).ToList());
    }

    private static AppointmentResult MapToResult(Appointment appointment)
    {
        return new AppointmentResult(
            appointment.Id,
            appointment.VanityId,
            appointment.Period,
            appointment.Status,
            appointment.ClientId,
            appointment.StaffMemberId,
            appointment.TreatmentId,
            appointment.SalonId
        );
    }

    private async Task<Appointment> GetAppointmentOrThrowAsync(Guid vanityId)
    {
        return await _appointmentRepository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Appointment", vanityId);
    }
}
