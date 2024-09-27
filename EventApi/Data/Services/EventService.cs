
using EventApi.Data.Interfaces;
using EventApi.Data.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Models.Dtos;
using System.Security.Claims;

namespace EventApi.Data.Services
{
    public class EventService(EventDbContext db) : IEventService
    {
        public async Task<IResult> CreateEventAsync(CreateEventRequest request, EventDbContext db, HttpContext ctx, CancellationToken cancellationToken = default)
        {
            var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Results.BadRequest(userId);

            var item = new Event(userId, request.Name, request.Description, request.StartDate);

            await db.Events.AddAsync(item);
            await db.SaveChangesAsync();

            return Results.Ok(item);
        }

        public async Task<IResult> GetByIdAsync(Guid id, EventDbContext db, CancellationToken cancellationToken = default)
        {
            var eventEntity = db.Events.FirstOrDefault(e => e.Id == id);

            if (eventEntity == null)
                return Results.BadRequest("Event not found");

            var response = new EventDto
            {
                Id = eventEntity.Id,
                AuthorId = eventEntity.AuthorId,
                Name = eventEntity.Name,
                Description = eventEntity.Description,
                CreatedDate = eventEntity.CreatedDate,
                StartDate = eventEntity.StartDate
            };
            return Results.Ok(response);
        }

        public async Task<IResult> GetAllEventsAsync(EventDbContext db, CancellationToken cancellationToken = default)
        {
            return Results.Ok(await db.Events.ToListAsync(cancellationToken));
        }

        public async Task<IResult> GetSubs(EventDbContext dbContext, HttpContext ctx, CancellationToken cancellationToken = default)
        {
            var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var subscribedEvents = await db.Subscriptions
                                   .Where(es => es.UserId == userId)
                                   .Include(es => es.Event)
                                   .Select(es => new SubsEventDto
                                   {
                                       Name = es.Event.Name,
                                       Description = es.Event.Description,
                                       StartDate = es.Event.StartDate,
                                   })
                                   .ToListAsync();

            return Results.Ok(subscribedEvents);
        }
    }
}
