namespace BeautyPlanner.BookingService.Application.Models;

public record CreateAppointmentModel(
    TimeRange Period,
    AppointmentStatus Status,
    Guid ClientId,
    Guid StaffMemberId,
    Guid TreatmentId,
    Guid SalonId
);