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
        var tenant = new Profession(model.Name, model.Description);

        await _repository.AddAsync(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<ProfessionResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<ProfessionResult>> UpdateProfessionAsync(UpdateProfessionModel model)
    {
        var tenant = await _repository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("Profession", model.VanityId);

        tenant.Update(model.Name, model.Description);
        _repository.Update(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<ProfessionResult>.Success(MapToResult(tenant));
    }

    public async Task DeleteProfessionAsync(Guid vanityId)
    {
        var tenant = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Profession", vanityId);

        _repository.Delete(tenant);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<ProfessionResult>> GetProfessionAsync(int id)
    {
        var tenant = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Profession", id);

        return Result<ProfessionResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<ProfessionResult>> GetProfessionAsync(Guid vanityId)
    {
        var tenant = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Profession", vanityId);

        return Result<ProfessionResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<List<ProfessionResult>>> GetProfessionsAsync()
    {
        var tenants = await _repository.ListAsync();

        return Result<List<ProfessionResult>>.Success(tenants.Select(MapToResult).ToList());
    }

    private static ProfessionResult MapToResult(Profession tenant)
    {
        return new ProfessionResult(
            tenant.Id,
            tenant.VanityId,
            tenant.Name,
            tenant.Description
        );
    }
}
