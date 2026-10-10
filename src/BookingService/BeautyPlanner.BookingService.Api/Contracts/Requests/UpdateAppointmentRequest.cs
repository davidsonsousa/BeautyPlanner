namespace BeautyPlanner.BookingService.Api.Contracts.Requests;

public record UpdateAppointmentRequest(
    int Id,
    Guid VanityId,
    TimeRange Period,
    AppointmentStatus Status,
    Guid ClientId,
    Guid StaffMemberId,
    Guid TreatmentId,
    Guid SalonId
);
