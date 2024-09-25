using EventApi.Data;
using EventApi.Data.Contracts;
using EventApi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventApi.endpoints
{
    public static class EventEdnpoints
    {
        public static void EventsEndpoits(this WebApplication app)
        {
            var events = app.MapGroup("events");

            events.MapPost("/create", async (IEventService service, CreateEventRequest request, EventDbContext db, CancellationToken cancellationToken = default) =>
            {
                return await service.CreateEventAsync(request, db, cancellationToken);
            });

            events.MapGet("/", async (IEventService service, [FromQuery] Guid id, EventDbContext db, CancellationToken cancellationToken = default) => 
            {
                return await service.GetByIdAsync(id, db, cancellationToken);
            });
        }
    }
}
