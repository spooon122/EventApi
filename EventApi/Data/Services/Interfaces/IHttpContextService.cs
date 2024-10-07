namespace EventApi.Data.Services.Interfaces;

public interface IHttpContextService
{
    string? GetCurrentUserId();
}