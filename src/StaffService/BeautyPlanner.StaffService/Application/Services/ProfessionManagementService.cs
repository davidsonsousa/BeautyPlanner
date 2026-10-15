namespace BeautyPlanner.StaffService.Application.Services;

public class ProfessionManagementService : IProfessionManagementService
{
    private readonly IRepository<Profession> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProfessionManagementService(IRepository<Profession> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ProfessionResult>> CreateProfessionAsync(CreateProfessionModel model)
    {
        var profession = new Profession(model.Name, model.Description);

        await _repository.AddAsync(profession);
        await _unitOfWork.SaveChangesAsync();

        return Result<ProfessionResult>.Success(profession.ToResult());
    }

    public async Task<Result> UpdateProfessionAsync(UpdateProfessionModel model)
    {
        var profession = await _repository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("Profession", model.VanityId);

        profession.Update(model.Name, model.Description);
        _repository.Update(profession);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task DeleteProfessionAsync(Guid vanityId)
    {
        var profession = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Profession", vanityId);

        _repository.Delete(profession);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<ProfessionResult>> GetProfessionAsync(int id)
    {
        var profession = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Profession", id);

        return Result<ProfessionResult>.Success(profession.ToResult());
    }

    public async Task<Result<ProfessionResult>> GetProfessionAsync(Guid vanityId)
    {
        var profession = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Profession", vanityId);

        return Result<ProfessionResult>.Success(profession.ToResult());
    }

    public async Task<Result<List<ProfessionResult>>> GetProfessionsAsync()
    {
        var professions = await _repository.ListAsync();

        return Result<List<ProfessionResult>>.Success(professions.ToResult());
    }
}
