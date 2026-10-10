namespace BeautyPlanner.BookingService.Api.Contracts.Responses;

public record AppointmentResponse(
    int Id,
    Guid VanityId,
    TimeRange Period,
    AppointmentStatus Status,
    Guid ClientId,
    Guid StaffMemberId,
    Guid TreatmentId,
    Guid SalonId
);
