var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pg");
var postgresdb = postgres.AddDatabase("postgres");

var eventsdb = postgres.AddDatabase("events");

builder.AddProject<Projects.EventApi>("event-api")
    .WithExternalHttpEndpoints()
    .WithReference(eventsdb)
    .WithReference(postgresdb);

builder.Build().Run();
