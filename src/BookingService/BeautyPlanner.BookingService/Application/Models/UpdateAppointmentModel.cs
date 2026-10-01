namespace BeautyPlanner.BookingService.Application.Models;

public record UpdateAppointmentModel(
    int Id,
    Guid VanityId,
    TimeRange Period,
    AppointmentStatus Status,
    Guid ClientId,
    Guid StaffMemberId,
    Guid TreatmentId,
    Guid SalonId
);
