dotnet tool restore

dotnet ef migrations add %nome% -o Persistence\Migrations --startup-project ..\GestaoEventos.Api\GestaoEventos.Api.csproj

dotnet ef --startup-project ..\GestaoEventos.Api\GestaoEventos.Api.csproj database update
