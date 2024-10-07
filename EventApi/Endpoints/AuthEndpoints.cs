using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventApi.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var auth = app.MapGroup("auth");

            auth.MapPost("/register", async ([FromBody] RegisterUserRequest request, UserManager<User> userManager) =>
            {
                var existedUser = await userManager.FindByEmailAsync(request.Email!);
                if (existedUser != null)
                    return Results.BadRequest("this user has been registered");

                var user = new User { UserName = request.Username, Email = request.Email };

                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return Results.BadRequest(result.Errors);
                }

                return Results.Ok("User successfully registered");
            });

            auth.MapPost("/login",
                async ([FromBody] LoginUserRequest request, [FromQuery] bool useCookies, UserManager<User> userManager,
                    SignInManager<User> signInManager) =>
                {
                    var user = await userManager.FindByEmailAsync(request.Email!);

                    if (user == null)
                        return Results.NotFound("User not found");

                    var result = await signInManager.PasswordSignInAsync(user, request.Password!,
                        isPersistent: useCookies, lockoutOnFailure: false);

                    if (!result.Succeeded)
                        return Results.BadRequest("Invalid login or password");

                    return Results.Ok("Successfully sing in");
                });

            auth.MapPost("/logout", async (SignInManager<User> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            }).RequireAuthorization();
        }
    }
}