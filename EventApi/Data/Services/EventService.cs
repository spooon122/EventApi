using EventApi.Data.Interfaces;
using EventApi.Data.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Models;
using Shared.Models.Models.Dtos;
using System.Security.Claims;
using EventApi.Data.Contracts;

namespace EventApi.Data.Services
{
    public class EventService : IEventService
    {
        public async Task<IResult> CreateEventAsync(CreateEventRequest request, EventDbContext db, HttpContext ctx, CancellationToken cancellationToken = default)
        {
            var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Results.Unauthorized();
            var item = new Event(request.Game, userId, request.Name, request.Description, request.StartDate);

            await db.Events.AddAsync(item, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return Results.Ok(new EventDto
            {
                Id = item.Id,
                Name = item.Name,
                AuthorId = item.AuthorId,
                GameEntity = item.GameEntity,
                Description = item.Description,
                CreatedDate = item.CreatedDate,
                StartDate = item.StartDate
            });
        }

        public IResult GetById(Guid id, EventDbContext db)
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
                StartDate = eventEntity.StartDate,
                GameEntity = eventEntity.GameEntity,
            };
            return Results.Ok(response);
        }

        public async Task<IResult> GetAllEventsAsync(EventDbContext db, CancellationToken cancellationToken = default)
        {
            return Results.Ok(await db.Events.ToListAsync(cancellationToken));
        }

        public async Task<IResult> GetSubs(EventDbContext db, HttpContext ctx, CancellationToken cancellationToken = default)
        {
            var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var subscribedEvents = await db.Subscriptions
                                   .Where(es => es.UserId == userId)
                                   .Include(es => es.Event)
                                   .Select(es => new SubsEventDto
                                   {
                                       Name = es.Event!.Name,
                                       Description = es.Event.Description,
                                       StartDate = es.Event.StartDate,
                                       Game = es.Event.GameEntity!
                                   })
                                   .ToListAsync(cancellationToken: cancellationToken);

            return Results.Ok(subscribedEvents);
        }

        
    }

    
}
