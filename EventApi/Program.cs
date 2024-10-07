using EventApi;
using EventApi.Data;
using EventApi.Data.Repositories;
using EventApi.Data.Repositories.Interfaces;
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
builder.Services.AddScoped<IHttpContextService, HttpContextService>();
builder.Services.AddScoped<ISubscribeService, SubscribeService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ISubscribeRepository, SubscribeRepository>();

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
app.UsersEndpoints();
app.MapAuthEndpoints();

app.Run();