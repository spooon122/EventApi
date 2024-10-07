namespace EventApi.Data.Contracts;

public record CreateEventRequest(string? Name, string? Game, string? Description, DateTime StartDate);