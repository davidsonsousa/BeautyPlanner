namespace BeautyPlanner.Shared.Infrastructure.Auth;

public class HttpUserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserContext(IHttpContextAccessor accessor)
    {
        _httpContextAccessor = accessor;
    }

    public string GetCurrentUsername()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "system";
    }
}
