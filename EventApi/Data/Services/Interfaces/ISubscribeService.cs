namespace EventApi.Data.Services.Interfaces;

public interface ISubscribeService
{
    public Task<IResult> SubscribeToEventAsync(Guid eventId, CancellationToken cancellationToken = default);

    public Task<IResult> UnsubscribeEventAsync(Guid eventId, CancellationToken cancellationToken = default);

    public Task<IResult> GetSubs(CancellationToken cancellationToken = default);
}