using System.Security.Claims;
using EventApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Models;

namespace EventApi.Data.Services;

public class SubscribeService : ISubscribeService
{
    public async Task<IResult> SubscribeToEventAsync(EventDbContext db, Guid eventId, HttpContext ctx, CancellationToken cancellationToken = default)
    {
        var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var subs = new Subscription
        {
            UserId = userId,
            EventId = eventId
        };

        await db.Subscriptions.AddAsync(subs, cancellationToken);

        var subscriptionHistory = new EventHistory
        {
            UserId = userId,
            EventId = eventId,
            SubscribedAt = DateTime.UtcNow
        };

        await db.EventsHistory.AddAsync(subscriptionHistory, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }

    public async Task<IResult> UnsubscribeEventAsync(EventDbContext db, Guid eventId, HttpContext ctx, CancellationToken cancellationToken = default)
    {
        var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var subscription = await db.Subscriptions
            .FirstOrDefaultAsync(s => s.EventId == eventId && s.UserId == userId, cancellationToken: cancellationToken);

        if (subscription == null)
        {
            return Results.NotFound("Subscription not found");
        }
        db.Subscriptions.Remove(subscription);

        var history = await db.EventsHistory
            .FirstOrDefaultAsync(h => h.UserId == userId && h.EventId == eventId && h.UnsubscribedAt == null, cancellationToken: cancellationToken);

        if (history != null)
        {
            history.UnsubscribedAt = DateTime.UtcNow;
            db.EventsHistory.Update(history);
        }
        await db.SaveChangesAsync(cancellationToken);

        return Results.Ok("Unsubscribed from event");
    }
}