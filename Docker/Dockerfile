# Multi-stage Dockerfile for API + Blazor WebAssembly static assets
# Assumes solution layout:
# - App.Api/
# - Todo.Frontend/
# Adjust project names/paths if they differ.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-frontend
WORKDIR /src
COPY ["Todo.Frontend/Todo.Frontend.csproj", "Todo.Frontend/"]
RUN dotnet restore "Todo.Frontend/Todo.Frontend.csproj"
COPY . .
WORKDIR "/src/Todo.Frontend"
RUN dotnet publish "Todo.Frontend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-api
WORKDIR /src
COPY ["App.Api/App.Api.csproj", "App.Api/"]
COPY ["App.Application/App.Application.csproj", "App.Application/"]
COPY ["App.Domain/App.Domain.csproj", "App.Domain/"]
COPY ["App.Infrastructure/App.Infrastructure.csproj", "App.Infrastructure/"]
RUN dotnet restore "App.Api/App.Api.csproj"
COPY . .
WORKDIR "/src/App.Api"
RUN dotnet publish "App.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy API binaries
COPY --from=build-api /app/publish .

# Copy Blazor WASM published static files into the API's wwwroot so the API serves the client
COPY --from=build-frontend /app/publish/wwwroot ./wwwroot

# Recommended: set the URL and expose ports
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "App.Api.dll"]