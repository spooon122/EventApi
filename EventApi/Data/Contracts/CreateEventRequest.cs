namespace EventApi.Data.Contracts
{
    public record CreateEventRequest(string? AuthorId, string? Name, string? Description, DateTime StartDate);
}
