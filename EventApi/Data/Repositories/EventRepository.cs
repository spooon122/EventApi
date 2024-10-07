using EventApi.Data.Contracts;
using EventApi.Data.Models.Dtos;
using EventApi.Data.Repositories.Interfaces;
using EventApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Data.Repositories;

public class EventRepository(EventDbContext context, IHttpContextService httpContextService) : IEventRepository
{
    private readonly IHttpContextService _httpContextService = httpContextService;
    private readonly EventDbContext _context = context;

    public async Task CreateAsync(CreateEventRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextService.GetCurrentUserId();
        var item = new Event(request.Game, userId, request.Name, request.Description, request.StartDate);
        await _context.Events.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<EventDto> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _context.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
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
        return await _context.Events.ToListAsync(cancellationToken: cancellationToken);
    }
}