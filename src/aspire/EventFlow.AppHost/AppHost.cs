var builder = DistributedApplication.CreateBuilder(args);

var keycloakUsername = builder.AddParameter("username", value: "admin");
var keycloakPassword = builder.AddParameter("password", secret: true, value: "admin");
var keycloak = builder.AddKeycloak("keycloak", 8080, keycloakUsername, keycloakPassword)
    .WithDataVolume("keycloak-eventos")
    .WithRealmImport("./Infra/Keycloak")
    .WithEnvironment("KC_HTTP_ENABLED", "true")
    .WithEnvironment("KC_PROXY_HEADERS", "xforwarded")
    .WithEnvironment("KC_HOSTNAME_STRICT", "false");

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume("postgres-eventos")
    .WithPgAdmin(pgAdmin => pgAdmin.WithHostPort(5050))
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitmqUsername = builder.AddParameter("username", value: "admin");
var rabbitmqPassword = builder.AddParameter("password", secret: true, value: "admin");
var rabbitmq = builder.AddRabbitMQ("messaging", rabbitmqUsername, rabbitmqPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("rabbitmq-eventos")
    .WithManagementPlugin();

var postgresdb = postgres.AddDatabase("postgresdb-eventos", "eventos");
var eventApi = builder.AddProject<Projects.EventFlow_Eventos_Api>("eventosapi")
    .WithReference(keycloak)
    .WithReference(postgresdb)
    .WithReference(rabbitmq)
    .WaitFor(keycloak)
    .WaitFor(postgresdb)
    .WaitFor(rabbitmq);

builder.Build().Run();