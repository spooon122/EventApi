using IdentityApi.DataAccess.Models;
using IdentityApi.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace IdentityApi
{
    public static class Extensions
    {
        public static IServiceCollection AddIdentityDb(this IServiceCollection services, WebApplication builder)
        {
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddDefaultTokenProviders();

            services.AddDbContext<UserDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString(nameof(UserDbContext));
                options.UseNpgsql(builder.Configuration.GetConnectionString(connectionString));
            });

            return services;
                                 
        }

        public static IServiceCollection AddCookieConfig(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                               opt.TokenLifespan = TimeSpan.FromMinutes(15));

            return services;
        }
    }
}
