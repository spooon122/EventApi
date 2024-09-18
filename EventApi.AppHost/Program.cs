var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var identityDb = postgres.AddDatabase("identitydb");

var identity = builder.AddProject<Projects.IdentityApi>("identity")
    .WithExternalHttpEndpoints()
    .WithReference(identityDb);

builder.AddProject<Projects.EventApi>("event-api")
    .WithExternalHttpEndpoints()
    .WithReference(identity);

builder.Build().Run();
