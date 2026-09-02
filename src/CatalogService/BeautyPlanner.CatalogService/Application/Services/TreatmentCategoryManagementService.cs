namespace BeautyPlanner.CatalogService.Application.Services;

public class TreatmentCategoryManagementService : ITreatmentCategoryManagementService
{
    private readonly IRepository<TreatmentCategory> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TreatmentCategoryManagementService(IRepository<TreatmentCategory> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TreatmentCategoryResult>> CreateTreatmentCategoryAsync(CreateTreatmentCategoryModel model)
    {
        var tenant = new TreatmentCategory(model.Name, model.Description);

        await _repository.AddAsync(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<TreatmentCategoryResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<TreatmentCategoryResult>> UpdateTreatmentCategoryAsync(UpdateTreatmentCategoryModel model)
    {
        var tenant = await _repository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("TreatmentCategory", model.VanityId);

        tenant.Update(model.Name, model.Description);
        _repository.Update(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<TreatmentCategoryResult>.Success(MapToResult(tenant));
    }

    public async Task DeleteTreatmentCategoryAsync(Guid vanityId)
    {
        var tenant = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("TreatmentCategory", vanityId);

        _repository.Delete(tenant);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<TreatmentCategoryResult>> GetTreatmentCategoryAsync(int id)
    {
        var tenant = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("TreatmentCategory", id);

        return Result<TreatmentCategoryResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<TreatmentCategoryResult>> GetTreatmentCategoryAsync(Guid vanityId)
    {
        var tenant = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("TreatmentCategory", vanityId);

        return Result<TreatmentCategoryResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<List<TreatmentCategoryResult>>> GetTreatmentCategorysAsync()
    {
        var tenants = await _repository.ListAsync();

        return Result<List<TreatmentCategoryResult>>.Success(tenants.Select(MapToResult).ToList());
    }

    private static TreatmentCategoryResult MapToResult(TreatmentCategory tenant)
    {
        return new TreatmentCategoryResult(
            tenant.Id,
            tenant.VanityId,
            tenant.Name,
            tenant.Description
        );
    }
}
