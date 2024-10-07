using EventApi;
using EventApi.Data;
using EventApi.Data.Services;
using EventApi.Data.Services.Interfaces;
using EventApi.Endpoints;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.AddAuthorization();

builder.Services.AddIdentityDb();
builder.Services.AddCookieConfig();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSwaggerGen();

builder.AddNpgsqlDbContext<EventDbContext>("events");
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

app.UseHttpsRedirection();

app.EventsEndpoints();
app.UsersEndpoits();
app.MapAuthEndpoints();

app.Run();