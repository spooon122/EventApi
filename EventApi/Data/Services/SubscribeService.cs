using System.Security.Claims;
using EventApi.Data.Repositories.Interfaces;
using EventApi.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Models;
using Shared.Models.Models.Dtos;

namespace EventApi.Data.Services;

public class SubscribeService(ISubscribeRepository subscribeRepository) : ISubscribeService
{
    public async Task<IResult> SubscribeToEventAsync(Guid eventId,
        CancellationToken cancellationToken = default)
    {
        await subscribeRepository.SubscribeToEventAsync(eventId, cancellationToken);
        return Results.Ok();
    }

    public async Task<IResult> UnsubscribeEventAsync(Guid eventId,
        CancellationToken cancellationToken = default)
    {
        var response = await subscribeRepository.UnsubscribeEventAsync(eventId, cancellationToken);
        return Results.Ok(response);
    }

    public async Task<IResult> GetSubs(
        CancellationToken cancellationToken = default)
    {
        var subscribeList = await subscribeRepository.GetSubs(cancellationToken);
        return Results.Ok(subscribeList);
    }
}