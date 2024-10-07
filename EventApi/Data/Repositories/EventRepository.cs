using EventApi.Data.Contracts;
using EventApi.Data.Models.Dtos;
using EventApi.Data.Repositories.Interfaces;
using EventApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data.Repositories;

public class EventRepository(EventDbContext context, IHttpContextService httpContextService) : IEventRepository
{
    public async Task<EventDto> CreateAsync(CreateEventRequest request, CancellationToken cancellationToken)
    {
        var userId = httpContextService.GetCurrentUserId();
        var item = new Event(request.Game, userId, request.Name, request.Description, request.StartDate);
        await context.Events.AddAsync(item, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return new EventDto
        {
            Id = item.Id,
            Name = item.Name,
            GameEntity = item.GameEntity,
            AuthorId = item.AuthorId,
            Description = item.Description,
            StartDate = item.StartDate
        };
    }

    public async Task<EventDto> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await context.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        if (item != null)
            return new EventDto
            {
                AuthorId = item.AuthorId,
                Description = item.Description,
                GameEntity = item.GameEntity,
                Id = item.Id,
                Name = item.Name,
                StartDate = item.StartDate
            };
        return new EventDto();
    }

    public async Task<List<Event>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Events.ToListAsync(cancellationToken: cancellationToken);
    }
}