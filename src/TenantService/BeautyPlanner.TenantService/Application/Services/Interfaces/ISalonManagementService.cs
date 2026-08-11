namespace BeautyPlanner.TenantService.Application.Services.Interfaces;

public interface ISalonManagementService
{
    Task<Result<SalonResult>> CreateSalonAsync(CreateSalonModel request);

    Task<Result<SalonResult>> UpdateSalonAsync(UpdateSalonModel request);

    Task DeleteSalonAsync(Guid vanityId);

    Task<Result<SalonResult>> GetSalonAsync(int id);

    Task<Result<SalonResult>> GetSalonAsync(Guid vanityId);

    Task<Result<List<SalonResult>>> GetSalonsAsync();
}
