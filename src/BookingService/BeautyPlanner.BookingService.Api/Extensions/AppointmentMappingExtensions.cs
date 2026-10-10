namespace BeautyPlanner.BookingService.Api.Extensions;

public static class AppointmentMappingExtensions
{
    public static CreateAppointmentModel ToModel(this CreateAppointmentRequest request)
    {
        return new CreateAppointmentModel(request.Period, request.Status, request.ClientId, request.StaffMemberId, request.TreatmentId, request.SalonId);
    }

    public static UpdateAppointmentModel ToModel(this UpdateAppointmentRequest request)
    {
        return new UpdateAppointmentModel(request.Id, request.VanityId, request.Period, request.Status, request.ClientId, request.StaffMemberId, request.TreatmentId, request.SalonId);
    }

    public static AppointmentResponse ToResponse(this AppointmentResult? result)
    {
        return new AppointmentResponse(result!.Id, result.VanityId, result.Period, result.Status, result.ClientId, result.StaffMemberId, result.TreatmentId, result.SalonId);
    }

    public static List<AppointmentResponse> ToResponse(this List<AppointmentResult>? result)
    {
        return result!.Select(ToResponse).ToList();
    }
}
