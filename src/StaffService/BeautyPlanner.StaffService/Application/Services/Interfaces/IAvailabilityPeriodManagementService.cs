namespace BeautyPlanner.StaffService.Application.Services.Interfaces;

public interface IAvailabilityPeriodManagementService
{
    Task<Result<AvailabilityPeriodResult>> CreateAvailabilityPeriodAsync(CreateAvailabilityPeriodModel request);

    Task<Result<AvailabilityPeriodResult>> UpdateAvailabilityPeriodAsync(UpdateAvailabilityPeriodModel request);

    Task DeleteAvailabilityPeriodAsync(Guid vanityId);

    Task<Result<AvailabilityPeriodResult>> GetAvailabilityPeriodAsync(int id);

    Task<Result<AvailabilityPeriodResult>> GetAvailabilityPeriodAsync(Guid vanityId);

    Task<Result<List<AvailabilityPeriodResult>>> GetAvailabilityPeriodsAsync();
}
