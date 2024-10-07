namespace EventApi.Data.Services.Interfaces;

public interface ISubscribeService
{
    public Task<IResult> SubscribeToEventAsync(EventDbContext db, Guid eventId, HttpContext ctx,
        CancellationToken cancellationToken = default);

    public Task<IResult> UnsubscribeEventAsync(EventDbContext db, Guid eventId, HttpContext ctx,
        CancellationToken cancellationToken = default);

    public Task<IResult> GetSubs(EventDbContext dbContext, HttpContext ctx,
        CancellationToken cancellationToken = default);
}