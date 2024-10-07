using EventApi.Data.Contracts;

namespace EventApi.Data.Services.Interfaces;

public interface IEventService
{
    public Task<IResult> CreateEventAsync(CreateEventRequest request, CancellationToken cancellationToken = default);

    public IResult GetEventById(Guid id);
    public Task<IResult> GetAllEventsAsync(CancellationToken cancellationToken = default);
}