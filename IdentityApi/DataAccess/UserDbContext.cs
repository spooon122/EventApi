using IdentityApi.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.DataAccess
{
    public class UserDbContext : IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
