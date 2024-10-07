using EventApi.Data;
using EventApi.Data.Contracts;
using EventApi.Data.Interfaces;

namespace EventApi.Endpoints
{
    public static class EventEndpoints
    {
        public static void EventsEndpoints(this WebApplication app)
        {
            var events = app.MapGroup("events").RequireAuthorization();

            events.MapPost("/create", async (IEventService service, CreateEventRequest request, EventDbContext db, 
                HttpContext ctx, CancellationToken cancellationToken = default) =>
            {
                return await service.CreateEventAsync(request, db, ctx, cancellationToken);
            });

            events.MapGet("/{id:guid}", (IEventService service, Guid id, EventDbContext db) => service.GetById(id, db));
            
            events.MapGet("/", async (IEventService service, EventDbContext db, 
                CancellationToken cancellationToken = default) =>
            {
                return await service.GetAllEventsAsync(db, cancellationToken);
            });

            events.MapPost("/{eventId:guid}/subscribe", async (ISubscribeService service, EventDbContext db, 
                Guid eventId, HttpContext ctx, CancellationToken cancellationToken = default) =>
            {
                return await service.SubscribeToEventAsync(db, eventId, ctx, cancellationToken);
            });

            events.MapGet("/subs", async (IEventService service, EventDbContext db, HttpContext ctx, 
                CancellationToken cancellationToken = default) =>
            {
                return await service.GetSubs(db, ctx, cancellationToken);
            });

            events.MapPost("/unsub/{eventId:guid}", async (ISubscribeService service, EventDbContext db, 
                Guid eventId, HttpContext ctx, CancellationToken cancellationToken = default) =>
            {
                return await service.UnsubscribeEventAsync(db, eventId, ctx, cancellationToken);
            });
        }
    }
}
