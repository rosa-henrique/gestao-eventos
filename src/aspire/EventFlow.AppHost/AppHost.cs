var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
    .ExcludeFromManifest()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithEnvironment("ACCEPT_EULA", "Y");

var keycloakUsername = builder.AddParameter("keycloakUsername", value: "admin");
var keycloakPassword = builder.AddParameter("keycloakPassword", secret: true, value: "admin");
var keycloak = builder.AddKeycloak("keycloak", 8080, keycloakUsername, keycloakPassword)
    .WithDataVolume("keycloak-eventos")
    .WithRealmImport("./Infra/Keycloak")
    .WithEnvironment("KC_HTTP_ENABLED", "true")
    .WithEnvironment("KC_PROXY_HEADERS", "xforwarded")
    .WithEnvironment("KC_HOSTNAME_STRICT", "false");

var sqlserver = builder.AddSqlServer("sqlserver")
    .WithDataVolume("sqlserver-eventos")
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitmqUsername = builder.AddParameter("rabbitmqUsername", value: "admin");
var rabbitmqPassword = builder.AddParameter("rabbitmqPassword", secret: true, value: "admin");
var rabbitmq = builder.AddRabbitMQ("messaging", rabbitmqUsername, rabbitmqPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("rabbitmq-eventos")
    .WithManagementPlugin();

var sqlserverEventos = sqlserver.AddDatabase("sqlserver-eventos", "eventos");
var eventoApi = builder.AddProject<Projects.EventFlow_Eventos_Api>("eventosapi")
    .WithReference(keycloak)
    .WithReference(sqlserverEventos)
    .WithReference(rabbitmq)
    .WithReference(seq)
    .WaitFor(keycloak)
    .WaitFor(sqlserverEventos)
    .WaitFor(rabbitmq)
    .WaitFor(seq);

var sqlserverInventario = sqlserver.AddDatabase("sqlserver-inventario", "inventario");
var inventarioApi = builder.AddProject<Projects.EventFlow_Inventario_Api>("inventarioapi")
    .WithReference(keycloak)
    .WithReference(sqlserverInventario)
    .WithReference(rabbitmq)
    .WithReference(seq)
    .WaitFor(keycloak)
    .WaitFor(sqlserverInventario)
    .WaitFor(rabbitmq)
    .WaitFor(seq);

builder.Build().Run();