var builder = DistributedApplication.CreateBuilder(args);

var keycloakUsername = builder.AddParameter("keycloakUsername", value: "admin");
var keycloakPassword = builder.AddParameter("keycloakPassword", secret: true, value: "admin");
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

var rabbitmqUsername = builder.AddParameter("rabbitmqUsername", value: "admin");
var rabbitmqPassword = builder.AddParameter("rabbitmqPassword", secret: true, value: "admin");
var rabbitmq = builder.AddRabbitMQ("messaging", rabbitmqUsername, rabbitmqPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("rabbitmq-eventos")
    .WithManagementPlugin();

var postgresdbEventos = postgres.AddDatabase("postgresdb-eventos", "eventos");
var eventoApi = builder.AddProject<Projects.EventFlow_Eventos_Api>("eventosapi")
    .WithReference(keycloak)
    .WithReference(postgresdbEventos)
    .WithReference(rabbitmq)
    .WaitFor(keycloak)
    .WaitFor(postgresdbEventos)
    .WaitFor(rabbitmq);

var postgresdbInventario = postgres.AddDatabase("postgresdb-inventario", "inventario");
var inventarioApi = builder.AddProject<Projects.EventFlow_Inventario_Api>("inventarioapi")
    .WithReference(keycloak)
    .WithReference(postgresdbInventario)
    .WithReference(rabbitmq)
    .WaitFor(keycloak)
    .WaitFor(postgresdbInventario)
    .WaitFor(rabbitmq);

builder.Build().Run();