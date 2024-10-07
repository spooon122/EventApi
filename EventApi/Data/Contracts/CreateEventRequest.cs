namespace EventApi.Data.Contracts;

public abstract record CreateEventRequest(string? Name, string? Game, string? Description, DateTime StartDate);