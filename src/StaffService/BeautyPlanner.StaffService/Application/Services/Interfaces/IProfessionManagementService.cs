namespace BeautyPlanner.StaffService.Application.Services.Interfaces;

public interface IProfessionManagementService
{
    Task<Result<ProfessionResult>> CreateProfessionAsync(CreateProfessionModel request);

    Task<Result<ProfessionResult>> UpdateProfessionAsync(UpdateProfessionModel request);

    Task DeleteProfessionAsync(Guid vanityId);

    Task<Result<ProfessionResult>> GetProfessionAsync(int id);

    Task<Result<ProfessionResult>> GetProfessionAsync(Guid vanityId);

    Task<Result<List<ProfessionResult>>> GetProfessionsAsync();
}
