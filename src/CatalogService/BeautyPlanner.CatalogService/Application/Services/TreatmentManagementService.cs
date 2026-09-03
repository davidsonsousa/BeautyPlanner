namespace BeautyPlanner.CatalogService.Application.Services;

public class TreatmentManagementService : ITreatmentManagementService
{
    private readonly ITreatmentRepository _treatmentRepository;
    private readonly IRepository<TreatmentCategory> _treatmentCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TreatmentManagementService(ITreatmentRepository repository, IRepository<TreatmentCategory> treatmentCategoryRepository, IUnitOfWork unitOfWork)
    {
        _treatmentRepository = repository;
        _treatmentCategoryRepository = treatmentCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TreatmentResult>> CreateTreatmentAsync(CreateTreatmentModel model)
    {
        var treatmentCategory = await _treatmentCategoryRepository.GetByVanityIdAsync(model.TreatmentCategoryId) ?? throw new NotFoundException("TreatmentCategory", model.TreatmentCategoryId);

        var treatment = new Treatment(model.Name, model.Description, model.Price, model.DurationInMinutes, model.SalonId, model.ProfessionId, treatmentCategory.Id);

        await _treatmentRepository.AddAsync(treatment);
        await _unitOfWork.SaveChangesAsync();

        return Result<TreatmentResult>.Success(MapToResult(treatment));
    }

    public async Task<Result<TreatmentResult>> UpdateTreatmentAsync(UpdateTreatmentModel model)
    {
        var treatment = await _treatmentRepository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("Treatment", model.VanityId);

        treatment.Update(model.Name, model.Description, model.Price, model.DurationInMinutes, model.ProfessionId, treatment.Id);
        _treatmentRepository.Update(treatment);
        await _unitOfWork.SaveChangesAsync();

        return Result<TreatmentResult>.Success(MapToResult(treatment));
    }

    public async Task DeleteTreatmentAsync(Guid vanityId)
    {
        var treatment = await _treatmentRepository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Treatment", vanityId);

        _treatmentRepository.Delete(treatment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<TreatmentResult>> GetTreatmentAsync(int id)
    {
        var treatment = await _treatmentRepository.GetByIdAsync(id) ?? throw new NotFoundException("Treatment", id);

        return Result<TreatmentResult>.Success(MapToResult(treatment));
    }

    public async Task<Result<TreatmentResult>> GetTreatmentAsync(Guid vanityId)
    {
        var treatment = await _treatmentRepository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Treatment", vanityId);

        return Result<TreatmentResult>.Success(MapToResult(treatment));
    }

    public async Task<Result<List<TreatmentResult>>> GetTreatmentsAsync()
    {
        var treatmentCategories = await _treatmentRepository.ListWithTreatmentCategoriesAsync();

        return Result<List<TreatmentResult>>.Success(treatmentCategories.Select(MapToResult).ToList());
    }

    private static TreatmentResult MapToResult(Treatment treatment)
    {
        return new TreatmentResult(
            treatment.Id,
            treatment.VanityId,
            treatment.Name,
            treatment.Description,
            treatment.Price,
            treatment.DurationInMinutes,
            treatment.ProfessionVanityId,
            treatment.TreatmentCategory.Name
        );
    }
}
