namespace BeautyPlanner.BookingService.Infrastructure.Persistence.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Appointment?> GetAppointmentByPeriodAsync(TimeRange timeRange)
    {
        return await GetValidRecords().Where(x => x.Period.Overlaps(timeRange)).FirstOrDefaultAsync();
    }
}
