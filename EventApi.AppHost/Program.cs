var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pg")
                      .WithPgAdmin();
var postgresdb = postgres.AddDatabase("postgres");

var identity = builder.AddProject<Projects.IdentityApi>("identity")
    .WithExternalHttpEndpoints()
    .WithReference(postgresdb);


builder.AddProject<Projects.EventApi>("event-api")
    .WithExternalHttpEndpoints()
    .WithReference(identity);

builder.Build().Run();
