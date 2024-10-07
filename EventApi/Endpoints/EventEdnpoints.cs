using EventApi.Data;
using EventApi.Data.Contracts;
using EventApi.Data.Services.Interfaces;

namespace EventApi.Endpoints;

public static class EventEndpoints
{
    public static void EventsEndpoints(this WebApplication app)
    {
        var events = app.MapGroup("events").RequireAuthorization();

        events.MapPost("/create", async (IEventService service, CreateEventRequest request, EventDbContext db,
                HttpContext ctx, CancellationToken cancellationToken = default) =>
            await service.CreateEventAsync(request, cancellationToken));

        events.MapGet("/{id:guid}",
            (IEventService service, Guid id, EventDbContext db) => service.GetEventById(id));

        events.MapGet("/", async (IEventService service, EventDbContext db,
                CancellationToken cancellationToken = default) =>
            await service.GetAllEventsAsync(cancellationToken));

        events.MapPost("/{eventId:guid}/subscribe",
            async (ISubscribeService service, EventDbContext db, Guid eventId, HttpContext ctx,
                    CancellationToken cancellationToken = default) =>
                await service.SubscribeToEventAsync(eventId, cancellationToken));

        events.MapGet("/subs", async (ISubscribeService service, EventDbContext db, HttpContext ctx,
            CancellationToken cancellationToken = default) => await service.GetSubs(cancellationToken));

        events.MapPost("/unsub/{eventId:guid}", async (ISubscribeService service, EventDbContext db,
                Guid eventId, HttpContext ctx, CancellationToken cancellationToken = default) =>
            await service.UnsubscribeEventAsync(eventId, cancellationToken));
    }
}