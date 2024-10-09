using System.Text.Json;
using EventApi.Data.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace EventApi.Data.Services
{
    public class UserService(IDistributedCache distributedCache, EventDbContext db) : IUserService
    {
        public async Task<IResult> GetUserById(string userId, UserManager<User> userManager)
        {
            User? user = null;

            var userString = await distributedCache.GetStringAsync(userId);

            if (userString != null)
                user = JsonSerializer.Deserialize<User>(userString);

            if (user == null)
            {
                user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    Console.WriteLine($"{user.UserName} извлечен из базы данных");
                    userString = JsonSerializer.Serialize(user);
                    await distributedCache.SetStringAsync(userId, userString, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                    });
                }
            }
            else
            {
                Console.WriteLine($"{user.UserName} извлечен из кэша");
            }

            return Results.Ok(await userManager.FindByIdAsync(userId));
        }

        public async Task<IResult> GetUserHistoryAsync(HttpContext httpContext, string? userId)
        {
            if (userId == null)
                return Results.BadRequest("User not found");

            var history = await db.EventsHistory.Where(h => h.UserId == userId).ToListAsync();

            return Results.Ok(history);
        }
    }
}