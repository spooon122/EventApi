using EventApi.Data;
using EventApi.Data.Interfaces;
using EventApi.Data.Services;
using EventApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddSwaggerGen();
builder.AddNpgsqlDbContext<EventDbContext>("events");
var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.EventsEndpoits();


app.Run();
