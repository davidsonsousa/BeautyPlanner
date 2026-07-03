namespace BeautyPlanner.Shared.Api;

public abstract class BaseController : ControllerBase
{
    protected readonly ILogger _logger;

    protected BaseController(ILoggerFactory loggerFactory, string category)
    {
        _logger = loggerFactory.CreateLogger(category);
    }

    protected void LogInfo(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    protected void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    protected void LogError(Exception ex, string message, params object[] args)
    {
        _logger.LogError(ex, message, args);
    }
}
