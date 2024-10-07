using System.Security.Claims;
using EventApi.Data.Services.Interfaces;

namespace EventApi.Data.Services;

public class HttpContextService(IHttpContextAccessor httpContextAccessor) : IHttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public string? GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}