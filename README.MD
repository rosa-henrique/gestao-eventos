dotnet tool restore

dotnet ef migrations add %nome% -o Persistence\Migrations --startup-project ..\GestaoEventos.Api\GestaoEventos.Api.csproj

dotnet ef --startup-project ..\GestaoEventos.Api\GestaoEventos.Api.csproj database update

gerar secret node -e "console.log(require('crypto').randomBytes(32).toString('hex'))"
