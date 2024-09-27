using EventApi.Data;

using EventApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Models.Models;
using System.Security.Claims;

namespace EventApi.Endpoints
{
    public static class EventEdnpoints
    {
        public static void EventsEndpoits(this WebApplication app)
        {
            var events = app.MapGroup("events").RequireAuthorization();

            events.MapPost("/create", async (IEventService service, CreateEventRequest request, EventDbContext db, HttpContext ctx, CancellationToken cancellationToken = default) =>
            {
                return await service.CreateEventAsync(request, db, ctx, cancellationToken);
            });

            events.MapGet("/{id}", async (IEventService service, Guid id, EventDbContext db, CancellationToken cancellationToken = default) => 
            {
                return await service.GetByIdAsync(id, db, cancellationToken);
            });

            events.MapGet("/", async (IEventService service, EventDbContext db, CancellationToken cancellationToken = default) =>
            {
                return await service.GetAllEventsAsync(db, cancellationToken);
            });

            events.MapPost("/subscribe", async (IEventService service, EventDbContext db, Guid eventId, HttpContext ctx, CancellationToken cancellationToken = default) =>
            {
                return await service.SubscribeToEventAsync(db, eventId ctx, cancellationToken);
            });

            events.MapGet("subs", async (IEventService service, EventDbContext db, HttpContext ctx, CancellationToken cancellationToken = default) =>
            {
                return await service.GetSubs(db, ctx, cancellationToken);
            });
        }
    }
}
