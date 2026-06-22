namespace BeautyPlanner.TenantService.Application.Services;

public class SalonService : ISalonService
{
    private readonly IRepository<Salon> _salonRepository;
    private readonly IRepository<Tenant> _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SalonService(IRepository<Salon> repository, IRepository<Tenant> tenantRepository, IUnitOfWork unitOfWork)
    {
        _salonRepository = repository;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<SalonResult>> CreateSalonAsync(CreateSalonModel model)
    {
        var tenant = await _tenantRepository.GetByVanityIdAsync(model.TenantId) ?? throw new NotFoundException("Tenant", model.TenantId);

        var salonAddress = new Address(model.Address.Line1, model.Address.Line2, model.Address.PostalCode,
                                  model.Address.City, model.Address.StateProvince, model.Address.Country);

        var salon = new Salon(model.Name, model.Description, model.Email, model.PhoneNumber, salonAddress, tenant.Id);

        await _salonRepository.AddAsync(salon);
        await _unitOfWork.SaveChangesAsync();

        return Result<SalonResult>.Success(MapToResult(salon));
    }

    public async Task<Result<SalonResult>> UpdateSalonAsync(UpdateSalonModel model)
    {
        var salon = await _salonRepository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("Salon", model.VanityId);
        
        var salonAddress = new Address(model.Address.Line1, model.Address.Line2, model.Address.PostalCode,
                                  model.Address.City, model.Address.StateProvince, model.Address.Country);

        salon.Update(model.Name, model.Description, model.Email, model.PhoneNumber, salonAddress);
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
