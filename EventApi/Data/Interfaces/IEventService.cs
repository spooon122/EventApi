using EventApi.Data.Contracts;
using EventApi.Data.Models;
using EventApi.Data.Models.Dtos;
using System;

namespace EventApi.Data.Interfaces
{
    public interface IEventService
    {
        public Task<IResult> CreateEventAsync(CreateEventRequest request, EventDbContext db, CancellationToken cancellationToken = default);
        public Task<IResult> GetByIdAsync(Guid id, EventDbContext db, CancellationToken cancellationToken = default);
    }
}
