var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pg");
var postgresdb = postgres.AddDatabase("postgres");

var eventsdb = postgres.AddDatabase("events");

var identity = builder.AddProject<Projects.IdentityApi>("identity")
    .WithExternalHttpEndpoints()
    .WithReference(postgresdb);


builder.AddProject<Projects.EventApi>("event-api")
    .WithExternalHttpEndpoints()
    .WithReference(eventsdb)
    .WithReference(identity);

builder.Build().Run();
