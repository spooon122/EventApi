using EventApi.Data.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data.Services
{
    public class UserService : IUserService
    {
        public IResult GetUserById(string userId, UserManager<User> userManager)
        {
            return Results.Ok(userManager.FindByIdAsync(userId));
        }

        public async Task<IResult> GetUserHistoryAsync(HttpContext httpContext, EventDbContext db, string? userId)
        {
            if (userId == null)
                return Results.BadRequest("User not found");

            var history = await db.EventsHistory.Where(h => h.UserId == userId).ToListAsync();

            return Results.Ok(history);
        }
    }
}