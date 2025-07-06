<div align="center">

[![GitHub license](https://img.shields.io/github/license/seu-usuario/gestao-eventos)](https://github.com/seu-usuario/gestao-eventos/blob/main/LICENSE)  
[![EF Core Version](https://img.shields.io/nuget/v/Microsoft.EntityFrameworkCore)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)

---

</div>

# Configuração do Ambiente ⚙️

## Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet)
- [Node.js](https://nodejs.org/) (apenas para geração de secrets)

## Comandos Essenciais

### 🔧 Restaurar Ferramentas
```shell
dotnet tool restore
```

### 🛠️ Gerenciamento de Migrations
#### Criar nova migration:
```shell
dotnet ef migrations add NomeDaMigration -o Persistence/Migrations --startup-project ../GestaoEventos.Api
```

#### Aplicar migrations:
```shell
dotnet ef database update --startup-project ../GestaoEventos.Api
```

### **Atenção**
Ambos comandos é preciso estar na pasta do projeto Infrastructure
```shell
cd src/GestaoEventos.Infrastructure
```

### 🔐 Geração de Secrets
Infortante para geração e validação de jwt, adicionar no arquivo appsettings.Development.json, seção JwtSettings.Secret
```shell
node -e "console.log(require('crypto').randomBytes(32).toString('hex'))"
```

dotnet tool install --global dotnet-sonarscanner
dotnet tool install --global dotnet-coverage
dotnet sonarscanner begin /k:"teste" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="<token>" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml /d:sonar.qualitygate.wait=true /d:sonar.scanner.scanAll=true /d:sonar.exclusions=**/Migrations/*.cs
dotnet build --no-incremental
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"  
dotnet sonarscanner end /d:sonar.token="<token>"