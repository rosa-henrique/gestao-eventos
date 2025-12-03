using Aspire.Hosting.Yarp.Transforms;

var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("seq")
    .ExcludeFromManifest()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithEnvironment("ACCEPT_EULA", "Y");

var keycloakUsername = builder.AddParameter("keycloakUsername", value: "admin");
var keycloakPassword = builder.AddParameter("keycloakPassword", secret: true, value: "admin");
var keycloak = builder.AddKeycloak("keycloak", 8081, keycloakUsername, keycloakPassword)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("keycloak-eventos")
    .WithRealmImport("./Infra/Keycloak")
    .WithEnvironment("KC_HTTP_ENABLED", "true")
    .WithEnvironment("KC_PROXY_HEADERS", "xforwarded")
    .WithEnvironment("KC_HOSTNAME_STRICT", "false")
    .WithEnvironment("KC_HOSTNAME", "http://localhost:8081");

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
var eventosApi = builder.AddProject<Projects.EventFlow_Eventos_Api>("eventosapi")
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

var sqlserverCompras = sqlserver.AddDatabase("sqlserver-compras", "compras");
var comprasApi = builder.AddProject<Projects.EventFlow_Compras_Api>("comprasapi")
    .WithReference(keycloak)
    .WithReference(sqlserverCompras)
    .WithReference(rabbitmq)
    .WithReference(seq)
    .WithReference(inventarioApi)
    .WaitFor(keycloak)
    .WaitFor(sqlserverCompras)
    .WaitFor(rabbitmq)
    .WaitFor(seq);

var gateway = builder.AddYarp("gateway")
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute("/auth/{**catch-all}", keycloak)
            .WithTransformPathRemovePrefix("/auth");

        yarp.AddRoute("/eventos/{**catch-all}", eventosApi)
            .WithTransformPathRemovePrefix("/eventos");

        yarp.AddRoute("/inventario/{**catch-all}", inventarioApi)
            .WithTransformPathRemovePrefix("/inventario");

        yarp.AddRoute("/compras/{**catch-all}", comprasApi)
            .WithTransformPathRemovePrefix("/compras");
    })
    .WithHostPort(8080);

builder.Build().Run();