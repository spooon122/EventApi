using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using EventApi.Data;

namespace EventApi
{
    public static class Extensions
    {
        public static IServiceCollection AddIdentityDb(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddDefaultTokenProviders();

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
                               opt.TokenLifespan = TimeSpan.FromMinutes(30));

            return services;
        }
    }
}
