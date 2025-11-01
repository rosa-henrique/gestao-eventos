var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", value: "admin");
var password = builder.AddParameter("password", secret: true, value: "admin");

var keycloak = builder.AddKeycloak("keycloak", 8080, username, password)
    .WithDataVolume("keycloak")
    .WithRealmImport("./Infra/Keycloak")
    .WithEnvironment("KC_HTTP_ENABLED", "true")
    .WithEnvironment("KC_PROXY_HEADERS", "xforwarded")
    .WithEnvironment("KC_HOSTNAME_STRICT", "false");

var eventApi = builder.AddProject<Projects.EventFlow_Eventos_Api>("eventosapi")
    .WithReference(keycloak)
    .WaitFor(keycloak);

builder.Build().Run();