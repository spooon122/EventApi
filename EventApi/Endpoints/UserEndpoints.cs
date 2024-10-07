using EventApi.Data;
using EventApi.Data.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EventApi.Endpoints;

public static class UserEndpoints
{
    public static void UsersEndpoints(this WebApplication app)
    {
        var users = app.MapGroup("users").RequireAuthorization();

        users.MapGet("/{userId}",
            (IUserService service, string userId, UserManager<User> userManager) =>
                service.GetUserById(userId, userManager));

        users.MapGet("/users/{userId}/history",
            async (string? userId, IUserService service, HttpContext httpContext, EventDbContext db) =>
                await service.GetUserHistoryAsync(httpContext, db, userId));
    }
}