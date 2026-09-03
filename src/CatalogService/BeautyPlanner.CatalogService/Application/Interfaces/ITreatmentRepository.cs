namespace BeautyPlanner.CatalogService.Application.Interfaces;

public interface ITreatmentRepository : IRepository<Treatment>
{
    Task<List<Treatment>> ListWithTreatmentCategoriesAsync(Expression<Func<Treatment, bool>>? filter = null);
}
