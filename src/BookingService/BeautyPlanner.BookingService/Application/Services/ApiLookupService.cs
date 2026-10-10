namespace BeautyPlanner.BookingService.Application.Services;

public class ApiLookupService : IApiLookupService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiLookupService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ClientLookupResult?> GetClientAsync(Guid vanityId)
    {
        return await GetDataAsync<ClientLookupResult>("ClientService", vanityId, "clients");
    }

    public async Task<StaffLookupResult?> GetStaffMemberAsync(Guid vanityId)
    {
        return await GetDataAsync<StaffLookupResult>("StaffService", vanityId, "staff-members");
    }

    public async Task<TreatmentLookupResult?> GetTreatmentAsync(Guid vanityId)
    {
        return await GetDataAsync<TreatmentLookupResult>("CatalogService", vanityId, "treatments");
    }

    private async Task<T?> GetDataAsync<T>(string clientName, Guid vanityId, string resource) where T : class
    {
        var httpClient = _httpClientFactory.CreateClient(clientName);
        var response = await httpClient.GetAsync($"v1/{resource}/{vanityId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>();
    }
}
