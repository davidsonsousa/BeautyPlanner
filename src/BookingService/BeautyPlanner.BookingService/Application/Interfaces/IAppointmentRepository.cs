namespace BeautyPlanner.BookingService.Application.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<Appointment?> GetAppointmentByPeriodAsync(TimeRange timeRange);
}
