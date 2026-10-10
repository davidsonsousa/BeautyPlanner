namespace BeautyPlanner.BookingService.Application.Services.Interfaces;

public interface IApiLookupService
{
    Task<ClientLookupResult?> GetClientAsync(Guid vanityId);

    Task<StaffLookupResult?> GetStaffMemberAsync(Guid vanityId);

    Task<TreatmentLookupResult?> GetTreatmentAsync(Guid vanityId);
}
