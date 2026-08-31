namespace BeautyPlanner.StaffService.Application.Services;

public class AvailabilityPeriodManagementService : IAvailabilityPeriodManagementService
{
    private readonly IAvailabilityPeriodRepository _repository;
    private readonly IStaffRepository _staffMemberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AvailabilityPeriodManagementService(IAvailabilityPeriodRepository repository, IStaffRepository staffMemberRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _staffMemberRepository = staffMemberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AvailabilityPeriodResult>> CreateAvailabilityPeriodAsync(CreateAvailabilityPeriodModel model)
    {
        var staffMember = await _staffMemberRepository.GetByVanityIdAsync(model.StaffMemberId) ?? throw new NotFoundException("StaffMember", model.StaffMemberId);

        var availabilityPeriod = new AvailabilityPeriod(model.DayOfWeek, model.StartTime, model.EndTime, staffMember.Id);

        await _repository.AddAsync(availabilityPeriod);
        await _unitOfWork.SaveChangesAsync();

        return Result<AvailabilityPeriodResult>.Success(MapToResult(availabilityPeriod));
    }

    public async Task<Result<AvailabilityPeriodResult>> UpdateAvailabilityPeriodAsync(UpdateAvailabilityPeriodModel model)
    {
        var availabilityPeriod = await _repository.GetByVanityIdAsync(model.VanityId) ?? throw new NotFoundException("AvailabilityPeriod", model.VanityId);

        availabilityPeriod.Update(model.DayOfWeek, model.StartTime, model.EndTime);
        _repository.Update(availabilityPeriod);
        await _unitOfWork.SaveChangesAsync();

        return Result<AvailabilityPeriodResult>.Success(MapToResult(availabilityPeriod));
    }

    public async Task DeleteAvailabilityPeriodAsync(Guid vanityId)
    {
        var availabilityPeriod = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("AvailabilityPeriod", vanityId);

        _repository.Delete(availabilityPeriod);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Result<AvailabilityPeriodResult>> GetAvailabilityPeriodAsync(int id)
    {
        var availabilityPeriod = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("AvailabilityPeriod", id);

        return Result<AvailabilityPeriodResult>.Success(MapToResult(availabilityPeriod));
    }

    public async Task<Result<AvailabilityPeriodResult>> GetAvailabilityPeriodAsync(Guid vanityId)
    {
        var availabilityPeriod = await _repository.GetByVanityIdAsync(vanityId) ?? throw new NotFoundException("AvailabilityPeriod", vanityId);

        return Result<AvailabilityPeriodResult>.Success(MapToResult(availabilityPeriod));
    }

    public async Task<Result<List<AvailabilityPeriodResult>>> GetAvailabilityPeriodsAsync()
    {
        var availabilityPeriods = await _repository.ListAsync();

        return Result<List<AvailabilityPeriodResult>>.Success(availabilityPeriods.Select(MapToResult).ToList());
    }

    public async Task<Result<List<AvailabilityPeriodResult>>> GetAvailabilityPeriodsForStaffMemberAsync(Guid staffMemberId)
    {
        var availabilityPeriods = await _repository.ListForStaffMember(staffMemberId);

        return Result<List<AvailabilityPeriodResult>>.Success(availabilityPeriods.Select(MapToResult).ToList());
    }

    private static AvailabilityPeriodResult MapToResult(AvailabilityPeriod availabilityPeriod)
    {
        return new AvailabilityPeriodResult(
            availabilityPeriod.Id,
            availabilityPeriod.VanityId,
            availabilityPeriod.DayOfWeek,
            availabilityPeriod.StartTime,
            availabilityPeriod.EndTime);
    }
}
