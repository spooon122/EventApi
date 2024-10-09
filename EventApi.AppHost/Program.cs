var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pg");
var postgresdb = postgres.AddDatabase("postgres");

var eventsdb = postgres.AddDatabase("events");
var cache = builder.AddRedis("cache");
builder.AddProject<Projects.EventApi>("event-api")
    .WithExternalHttpEndpoints()
    .WithReference(eventsdb)
    .WithReference(postgresdb)
    .WithReference(cache);

builder.Build().Run();