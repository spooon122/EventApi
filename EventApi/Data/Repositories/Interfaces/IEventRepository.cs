using EventApi.Data.Contracts;
using EventApi.Data.Models.Dtos;

namespace EventApi.Data.Repositories.Interfaces;

public interface IEventRepository
{
    public Task CreateAsync(CreateEventRequest request, CancellationToken cancellationToken = default);
    public Task<EventDto> GetById(Guid id, CancellationToken cancellationToken = default);
    public Task<List<Event>> GetAllAsync(CancellationToken cancellationToken = default);
}