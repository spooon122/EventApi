using EventApi.Data.Repositories.Interfaces;
using EventApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Models;
using Shared.Models.Models.Dtos;

namespace EventApi.Data.Repositories;

public class SubscribeRepository(EventDbContext context, IHttpContextService httpContextService) : ISubscribeRepository
{
    public async Task SubscribeToEventAsync(Guid eventId,
        CancellationToken cancellationToken = default)
    {
        var userId = httpContextService.GetCurrentUserId();

        var subs = new Subscription
        {
            EventId = eventId,
            UserId = userId
        };

        await context.Subscriptions.AddAsync(subs, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        var history = new EventHistory
        {
            EventId = eventId,
            UserId = userId,
            SubscribedAt = DateTime.UtcNow
        };

        await context.EventsHistory.AddAsync(history, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UnsubscribeEventAsync(Guid eventId,
        CancellationToken cancellationToken = default)
    {
        var userId = httpContextService.GetCurrentUserId();

        var subscription = await
            context.Subscriptions.FirstOrDefaultAsync(s => s.EventId == eventId && s.UserId == userId,
                cancellationToken);
        if (subscription == null) await Task.FromException(new Exception("Subscription not found"));
        context.Subscriptions.Remove(subscription!);

        var history = await context.EventsHistory.FirstOrDefaultAsync(h => h.EventId == eventId && h.UserId == userId,
            cancellationToken);
        if (history != null)
        {
            history.UnsubscribedAt = DateTime.UtcNow;
            context.EventsHistory.Update(history);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<SubsEventDto>> GetSubs(CancellationToken cancellationToken = default)
    {
        var userId = httpContextService.GetCurrentUserId();
        var subscribedEvents = await context.Subscriptions
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
        return subscribedEvents;
    }
}