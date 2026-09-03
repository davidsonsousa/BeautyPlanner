namespace BeautyPlanner.CatalogService.Application.Services.Interfaces;

public interface ITreatmentManagementService
{
    Task<Result<TreatmentResult>> CreateTreatmentAsync(CreateTreatmentModel request);

    Task<Result<TreatmentResult>> UpdateTreatmentAsync(UpdateTreatmentModel request);

    Task DeleteTreatmentAsync(Guid vanityId);

    Task<Result<TreatmentResult>> GetTreatmentAsync(int id);

    Task<Result<TreatmentResult>> GetTreatmentAsync(Guid vanityId);

    Task<Result<List<TreatmentResult>>> GetTreatmentsAsync();
}
