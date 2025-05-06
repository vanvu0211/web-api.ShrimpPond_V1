
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ShrimpPond.API/ShrimpPond.API.csproj", "ShrimpPond.API/"]
COPY ["ShrimpPond.Application/ShrimpPond.Application.csproj", "ShrimpPond.Application/"]
COPY ["ShrimpPond.Domain/ShrimpPond.Domain.csproj", "ShrimpPond.Domain/"]
COPY ["ShrimpPond.Infrastructure/ShrimpPond.Infrastructure.csproj", "ShrimpPond.Infrastructure/"]
COPY ["ShrimpPond.Persistence/ShrimpPond.Persistence.csproj", "ShrimpPond.Persistence/"]
RUN dotnet restore "./ShrimpPond.API/ShrimpPond.API.csproj"
COPY . .
WORKDIR "/src/ShrimpPond.API"
RUN dotnet build "./ShrimpPond.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShrimpPond.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShrimpPond.API.dll"]