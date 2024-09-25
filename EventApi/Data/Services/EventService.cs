using EventApi.Data.Contracts;
using EventApi.Data.Interfaces;
using EventApi.Data.Models;
using EventApi.Data.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data.Services
{
    public class EventService(EventDbContext db) : IEventService
    {
        public async Task<IResult> CreateEventAsync(CreateEventRequest request, EventDbContext db, CancellationToken cancellationToken = default)
        {
            var item = new Event(request.AuthorId, request.Name, request.Description, request.StartDate);

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
    }
}
