using EventApi.Data.Contracts;

namespace EventApi.Data.Interfaces
{
    public interface IEventService
    {
        public Task<IResult> CreateEventAsync(CreateEventRequest request, EventDbContext db, 
            HttpContext ctx, CancellationToken cancellationToken = default);
        public IResult GetById(Guid id, EventDbContext db);
        public Task<IResult> GetAllEventsAsync(EventDbContext db, CancellationToken cancellationToken = default);

        public Task<IResult> GetSubs(EventDbContext dbContext, HttpContext ctx,
            CancellationToken cancellationToken = default);
    }
}
