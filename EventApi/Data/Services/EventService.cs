using EventApi.Data.Contracts;
using EventApi.Data.Repositories.Interfaces;
using EventApi.Data.Services.Interfaces;

namespace EventApi.Data.Services
{
    public class EventService(IEventRepository repository) : IEventService
    {
        public async Task<IResult> CreateEventAsync(CreateEventRequest request,
            CancellationToken cancellationToken = default)
        {
            return Results.Ok(await repository.CreateAsync(request, cancellationToken));
        }

        public async Task<IResult> GetEventById(Guid id)
        {
            var eventEntity = await repository.GetById(id);


            return Results.Ok(eventEntity);
        }

        public async Task<IResult> GetAllEventsAsync(CancellationToken cancellationToken = default)
        {
            return Results.Ok(await repository.GetAllAsync(cancellationToken: cancellationToken));
        }
    }
}