using IdentityApi.DataAccess.Contracts;
using IdentityApi.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var auth = app.MapGroup("auth");

            auth.MapPost("/register", async ([FromBody] RegisterUserRequest request, UserManager<User> userManager) =>
            {
                var existedUser = userManager.FindByEmailAsync(request.Email!);
                
                var user = new User { UserName = request.Username, Email = request.Email};

                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded) 
                {
                    return Results.BadRequest(result.Errors);
                }
                return Results.Ok("User successfully registered");
            });

            auth.MapPost("/login", async ([FromBody] LoginUserRequest request, UserManager<User> userManager, SignInManager<User> signInManager) =>
            {
                var user = await userManager.FindByEmailAsync(request.Email!);

                if (user == null)
                    return Results.NotFound("User not found");

                var result = await signInManager.PasswordSignInAsync(user, request.Password!, isPersistent: request.RememberMe, lockoutOnFailure: false);

                if (!result.Succeeded)
                    return Results.BadRequest("Invalid login or password");

                return Results.Ok("Successfully sing in");
            });
        }
    }
}
