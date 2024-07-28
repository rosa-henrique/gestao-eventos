dotnet tool restore

dotnet ef --startup-project ..\GestaoEventos.Api\GestaoEventos.Api.csproj migrations add %nome%

dotnet ef --startup-project ..\GestaoEventos.Api\GestaoEventos.Api.csproj database update