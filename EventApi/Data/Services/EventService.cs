using EventApi.Data.Models.Dtos;
using System.Security.Claims;
using EventApi.Data.Contracts;
using EventApi.Data.Repositories.Interfaces;
using EventApi.Data.Services.Interfaces;

namespace EventApi.Data.Services
{
    public class EventService(IEventRepository repository) : IEventService
    {
        private readonly IEventRepository _repository = repository;

        public async Task<IResult> CreateEventAsync(CreateEventRequest request,
            CancellationToken cancellationToken = default)
        {
            await _repository.CreateAsync(request, cancellationToken);
            return Results.Ok("Event created");
        }

        public IResult GetEventById(Guid id)
        {
            var eventEntity = _repository.GetById(id);

            if (!eventEntity.IsCompletedSuccessfully)
                return Results.BadRequest(eventEntity.Exception?.Message);

            return Results.Ok(eventEntity);
        }

        public async Task<IResult> GetAllEventsAsync(CancellationToken cancellationToken = default)
        {
            return Results.Ok(await _repository.GetAllAsync(cancellationToken: cancellationToken));
        }
    }
}