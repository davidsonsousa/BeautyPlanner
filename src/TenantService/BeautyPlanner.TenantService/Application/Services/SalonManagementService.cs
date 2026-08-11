namespace BeautyPlanner.TenantService.Application.Services;

public class SalonManagementService : ISalonManagementService
{
    private readonly IRepository<Salon> _salonRepository;
    private readonly IRepository<Tenant> _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SalonManagementService(IRepository<Salon> repository, IRepository<Tenant> tenantRepository, IUnitOfWork unitOfWork)
    {
        _salonRepository = repository;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SalonResult>> CreateSalonAsync(CreateSalonModel model)
    {
        var tenant = await _tenantRepository.GetByVanityIdAsync(model.TenantId) ?? throw new NotFoundException("Tenant", model.TenantId);

        var salon = new Salon(model.Name, model.Description, model.Email, model.PhoneNumber, PrepareAddress(model.Address), tenant.Id);

        await _salonRepository.AddAsync(salon);
        await _unitOfWork.SaveChangesAsync();

        return Result<SalonResult>.Success(MapToResult(salon));
    }

    public async Task<Result<SalonResult>> UpdateSalonAsync(UpdateSalonModel model)
    {
        var salon = await _salonRepository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("Salon", model.VanityId);

        salon.Update(model.Name, model.Description, model.Email, model.PhoneNumber, PrepareAddress(model.Address));
        _salonRepository.Update(salon);
        await _unitOfWork.SaveChangesAsync();

        return Result<SalonResult>.Success(MapToResult(salon));
    }

    public async Task DeleteSalonAsync(Guid vanityId)
    {
        var salon = await _salonRepository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Salon", vanityId);

        _salonRepository.Delete(salon);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<SalonResult>> GetSalonAsync(int id)
    {
        var salon = await _salonRepository.GetByIdAsync(id) ?? throw new NotFoundException("Salon", id);

        return Result<SalonResult>.Success(MapToResult(salon));
    }

    public async Task<Result<SalonResult>> GetSalonAsync(Guid vanityId)
    {
        var salon = await _salonRepository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("Salon", vanityId);

        return Result<SalonResult>.Success(MapToResult(salon));
    }

    public async Task<Result<List<SalonResult>>> GetSalonsAsync()
    {
        var tenants = await _salonRepository.ListAsync();

        return Result<List<SalonResult>>.Success(tenants.Select(MapToResult).ToList());
    }

    private static Address PrepareAddress(AddressModel model)
    {
        return new Address(model.Line1, model.Line2, model.PostalCode, model.City, model.StateProvince, model.Country);
    }

    private static SalonResult MapToResult(Salon salon)
    {
        var address = new AddressModel(salon.Address.Line1, salon.Address.Line2, salon.Address.PostalCode,
                                       salon.Address.City, salon.Address.StateProvince, salon.Address.Country);

        return new SalonResult(
            salon.Id,
            salon.VanityId,
            salon.Name,
            salon.Description,
            salon.Email,
            salon.PhoneNumber,
            address
        );
    }
}
