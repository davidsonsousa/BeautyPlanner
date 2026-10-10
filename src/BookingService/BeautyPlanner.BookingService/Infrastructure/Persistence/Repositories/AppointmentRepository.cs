namespace BeautyPlanner.BookingService.Infrastructure.Persistence.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(BookingDbContext dbContext) : base(dbContext)
    {
    }
}
