FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GestaoEventos.Api/GestaoEventos.Api.csproj", "src/GestaoEventos.Api/"]
COPY ["src/GestaoEventos.Application/GestaoEventos.Application.csproj", "src/GestaoEventos.Application/"]
COPY ["src/GestaoEventos.Domain/GestaoEventos.Domain.csproj", "src/GestaoEventos.Domain/"]
COPY ["src/GestaoEventos.Infrastructure/GestaoEventos.Infrastructure.csproj", "src/GestaoEventos.Infrastructure/"]
RUN dotnet restore "src/GestaoEventos.Api/GestaoEventos.Api.csproj"
COPY . .
WORKDIR "/src/src/GestaoEventos.Api"
RUN dotnet build "./GestaoEventos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GestaoEventos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestaoEventos.Api.dll"]
