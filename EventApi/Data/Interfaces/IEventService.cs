using EventApi.Data.Contracts;

namespace EventApi.Data.Interfaces
{
    public interface IEventService
    {
        public Task<IResult> CreateEventAsync(CreateEventRequest request, EventDbContext db, CancellationToken cancellationToken = default);
        public Task<IResult> GetByIdAsync(Guid id, EventDbContext db, CancellationToken cancellationToken = default);
        public Task<IResult> GetAllEventsAsync(EventDbContext db, CancellationToken cancellationToken = default);
    }
}
