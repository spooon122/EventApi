
namespace EventApi.Data.Interfaces
{
    public interface IEventService
    {
        public Task<IResult> CreateEventAsync(CreateEventRequest request, EventDbContext db, HttpContext ctx, CancellationToken cancellationToken = default);
        public Task<IResult> GetByIdAsync(Guid id, EventDbContext db, CancellationToken cancellationToken = default);
        public Task<IResult> GetAllEventsAsync(EventDbContext db, CancellationToken cancellationToken = default);
        public Task<IResult> GetSubs(EventDbContext dbContext, HttpContext ctx, CancellationToken cancellationToken = default);
        public Task<IResult> SubscribeToEventAsync(EventDbContext db, Guid eventId, HttpContext ctx, CancellationToken cancellationToken = default);
    }
}
