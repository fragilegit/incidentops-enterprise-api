FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY IncidentOps.sln .
COPY IncidentOps.Domain/IncidentOps.Domain.csproj IncidentOps.Domain/
COPY IncidentOps.Application/IncidentOps.Application.csproj IncidentOps.Application/
COPY IncidentOps.Infrastructure/IncidentOps.Infrastructure.csproj IncidentOps.Infrastructure/
COPY IncidentOps.Api/IncidentOps.Api.csproj IncidentOps.Api/

RUN dotnet restore

COPY . .

RUN dotnet publish IncidentOps.Api/IncidentOps.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "IncidentOps.Api.dll"]
