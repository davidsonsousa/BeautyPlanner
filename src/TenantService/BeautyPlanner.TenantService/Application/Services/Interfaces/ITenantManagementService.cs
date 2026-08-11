namespace BeautyPlanner.TenantService.Application.Services.Interfaces;

public interface ITenantManagementService
{
    Task<Result<TenantResult>> CreateTenantAsync(CreateTenantModel request);

    Task<Result<TenantResult>> UpdateTenantAsync(UpdateTenantModel request);

    Task DeleteTenantAsync(Guid vanityId);

    Task<Result<TenantResult>> GetTenantAsync(int id);

    Task<Result<TenantResult>> GetTenantAsync(Guid vanityId);

    Task<Result<List<TenantResult>>> GetTenantsAsync();
}
