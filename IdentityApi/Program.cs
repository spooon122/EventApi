using IdentityApi;
using IdentityApi.DataAccess;
using IdentityApi.Endpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.AddAuthorization();

builder.Services.AddIdentityDb();
builder.Services.AddCookieConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.AddNpgsqlDbContext<UserDbContext>("postgres");

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();


app.Run();
