using EventApi.Data.Interfaces;
using EventApi.Data;
using Microsoft.AspNetCore.Identity;

namespace EventApi.Endpoints
{
    public static class UserEndpoints
    {
        public static void UsersEndpoits(this WebApplication app)
        {
            var users = app.MapGroup("users").RequireAuthorization();

            users.MapGet("/{userId}", (IUserService service, string userId, UserManager<User> userManager) =>
            {
                return service.GetUserById(userId, userManager);
            });

            users.MapGet("/users/{userId}/history", async (string? userId, IUserService service, HttpContext httpContext, EventDbContext db) =>
            {
                return await service.GetUserHistoryAsync(httpContext, db, userId);
            });
        }
    }
}
