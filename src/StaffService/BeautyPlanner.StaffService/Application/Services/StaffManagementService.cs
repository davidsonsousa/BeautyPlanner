namespace BeautyPlanner.StaffService.Application.Services;

public class StaffManagementService : IStaffManagementService
{
    private readonly IRepository<StaffMember> _staffRepository;
    private readonly IRepository<Profession> _professionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StaffManagementService(IRepository<StaffMember> repository, IRepository<Profession> professionRepository, IUnitOfWork unitOfWork)
    {
        _staffRepository = repository;
        _professionRepository = professionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<StaffMemberResult>> CreateStaffMemberAsync(CreateStaffMemberModel model)
    {
        var profession = await _professionRepository.GetByVanityIdAsync(model.ProfessionId) ?? throw new NotFoundException("Profession", model.ProfessionId);

        var staffMember = new StaffMember(model.FirstName, model.LastName, model.Email, model.PhoneNumber, profession.Id, model.DateOfBirth,
                                          PrepareAddress(model.Address), model.SalonId);

        await _staffRepository.AddAsync(staffMember);
        await _unitOfWork.SaveChangesAsync();

        return Result<StaffMemberResult>.Success(MapToResult(staffMember));
    }

    public async Task<Result<StaffMemberResult>> UpdateStaffMemberAsync(UpdateStaffMemberModel model)
    {
        var profession = await _professionRepository.GetByVanityIdAsync(model.ProfessionId) ?? throw new NotFoundException("Profession", model.ProfessionId);

        var staffMember = await _staffRepository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("StaffMember", model.VanityId);

        staffMember.Update(model.FirstName, model.LastName, model.Email, model.PhoneNumber, profession.Id, model.DateOfBirth,
                                          PrepareAddress(model.Address));
        _staffRepository.Update(staffMember);
        await _unitOfWork.SaveChangesAsync();

        return Result<StaffMemberResult>.Success(MapToResult(staffMember));
    }

    public async Task DeleteStaffMemberAsync(Guid vanityId)
    {
        var staffMember = await _staffRepository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("StaffMember", vanityId);

        _staffRepository.Delete(staffMember);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<StaffMemberResult>> GetStaffMemberAsync(int id)
    {
        var staffMember = await _staffRepository.GetByIdAsync(id) ?? throw new NotFoundException("StaffMember", id);

        return Result<StaffMemberResult>.Success(MapToResult(staffMember));
    }

    public async Task<Result<StaffMemberResult>> GetStaffMemberAsync(Guid vanityId)
    {
        var staffMember = await _staffRepository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("StaffMember", vanityId);

        return Result<StaffMemberResult>.Success(MapToResult(staffMember));
    }

    public async Task<Result<List<StaffMemberResult>>> GetStaffMembersAsync()
    {
        var tenants = await _staffRepository.ListAsync();

        return Result<List<StaffMemberResult>>.Success(tenants.Select(MapToResult).ToList());
    }

    private static Address PrepareAddress(AddressModel model)
    {
        return new Address(model.Line1, model.Line2, model.PostalCode, model.City, model.StateProvince, model.Country);
    }

    private static StaffMemberResult MapToResult(StaffMember staffMember)
    {
        var address = new AddressModel(staffMember.Address.Line1, staffMember.Address.Line2, staffMember.Address.PostalCode,
                                       staffMember.Address.City, staffMember.Address.StateProvince, staffMember.Address.Country);

        return new StaffMemberResult(
            staffMember.Id,
            staffMember.VanityId,
            staffMember.FirstName,
            staffMember.LastName,
            staffMember.Email,
            staffMember.PhoneNumber,
            staffMember.Profession.Name,
            staffMember.DateOfBirth,
            address
        );
    }
}
