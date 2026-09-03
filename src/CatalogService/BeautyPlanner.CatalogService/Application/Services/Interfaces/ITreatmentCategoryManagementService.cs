namespace BeautyPlanner.CatalogService.Application.Services.Interfaces;

public interface ITreatmentCategoryManagementService
{
    Task<Result<TreatmentCategoryResult>> CreateTreatmentCategoryAsync(CreateTreatmentCategoryModel request);

    Task<Result<TreatmentCategoryResult>> UpdateTreatmentCategoryAsync(UpdateTreatmentCategoryModel request);

    Task DeleteTreatmentCategoryAsync(Guid vanityId);

    Task<Result<TreatmentCategoryResult>> GetTreatmentCategoryAsync(int id);

    Task<Result<TreatmentCategoryResult>> GetTreatmentCategoryAsync(Guid vanityId);

    Task<Result<List<TreatmentCategoryResult>>> GetTreatmentCategorysAsync();
}
