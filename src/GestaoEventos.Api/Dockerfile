FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GestaoEventos.Api/GestaoEventos.Api.csproj", "src/GestaoEventos.Api/"]
RUN dotnet restore "src/GestaoEventos.Api/GestaoEventos.Api.csproj" -p:HUSKY=0
COPY src/GestaoEventos.Api/GestaoEventos.Api.csproj src/GestaoEventos.Api/
COPY src/GestaoEventos.Domain/GestaoEventos.Domain.csproj src/GestaoEventos.Domain/
COPY src/GestaoEventos.Application/GestaoEventos.Application.csproj src/GestaoEventos.Application/
COPY src/GestaoEventos.Infrastructure/GestaoEventos.Infrastructure.csproj src/GestaoEventos.Infrastructure/
COPY Directory.Build.props .
WORKDIR "/src/src/GestaoEventos.Api"
RUN dotnet build "./GestaoEventos.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/build -p:HUSKY=0

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GestaoEventos.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false -p:HUSKY=0

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER nonroot
ENTRYPOINT ["dotnet", "GestaoEventos.Api.dll"]
