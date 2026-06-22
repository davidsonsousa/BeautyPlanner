namespace BeautyPlanner.TenantService.Application.Services;

public class TenantService : ITenantService
{
    private readonly IRepository<Tenant> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public TenantService(IRepository<Tenant> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TenantResult>> CreateTenantAsync(CreateTenantModel model)
    {
        var tenant = new Tenant(model.Name, model.Description);

        await _repository.AddAsync(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<TenantResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<TenantResult>> UpdateTenantAsync(UpdateTenantModel model)
    {
        var tenant = await _repository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("Tenant", model.VanityId);

        tenant.Update(model.Name, model.Description);
        _repository.Update(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<TenantResult>.Success(MapToResult(tenant));
    }

    public async Task DeleteTenantAsync(Guid vanityId)
    {
        var tenant = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Tenant", vanityId);

        _repository.Delete(tenant);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<TenantResult>> GetTenantAsync(int id)
    {
        var tenant = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Tenant", id);

        return Result<TenantResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<TenantResult>> GetTenantAsync(Guid vanityId)
    {
        var tenant = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Tenant", vanityId);

        return Result<TenantResult>.Success(MapToResult(tenant));
    }

    public async Task<Result<List<TenantResult>>> GetTenantsAsync()
    {
        var tenants = await _repository.ListAsync();

        return Result<List<TenantResult>>.Success(tenants.Select(MapToResult).ToList());
    }

    private static TenantResult MapToResult(Tenant tenant)
    {
        return new TenantResult(
            tenant.Id,
            tenant.VanityId,
            tenant.Name,
            tenant.Description
        );
    }
}
