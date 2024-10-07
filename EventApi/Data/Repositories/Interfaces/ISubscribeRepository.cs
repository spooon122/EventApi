using Shared.Models.Models.Dtos;

namespace EventApi.Data.Repositories.Interfaces;

public interface ISubscribeRepository
{
    public Task SubscribeToEventAsync(Guid eventId,
        CancellationToken cancellationToken = default);

    public Task<string> UnsubscribeEventAsync(Guid eventId,
        CancellationToken cancellationToken = default);

    public Task<List<SubsEventDto>> GetSubs(CancellationToken cancellationToken = default);
}