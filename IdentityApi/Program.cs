using IdentityApi;
using IdentityApi.DataAccess;
using IdentityApi.Endpoints;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
builder.AddNpgsqlDbContext<UserDbContext>("postgres");
builder.Services.AddAuthorization();

builder.Services.AddIdentityDb();
builder.Services.AddCookieConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    // Retrieve an instance of the DbContext class and manually run migrations during development
    using (var scope = app.Services.CreateScope())
    {
        await using var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        await context.Database.EnsureCreatedAsync();
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();


app.Run();
