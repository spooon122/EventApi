using Microsoft.AspNetCore.Identity;

namespace EventApi.Data.Interfaces
{
    public interface IUserService
    {
        public IResult GetUserById(string userId, UserManager<User> userManager);
        public Task<IResult> GetUserHistoryAsync(HttpContext httpContext, EventDbContext db, string? userId);
    }
}