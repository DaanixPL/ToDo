# Backend: build and publish App.Api, include appsettings files (can be overridden by env vars)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["App.Api/App.Api.csproj", "App.Api/"]
COPY ["App.Application/App.Application.csproj", "App.Application/"]
COPY ["App.Domain/App.Domain.csproj", "App.Domain/"]
COPY ["App.Infrastructure/App.Infrastructure.csproj", "App.Infrastructure/"]
RUN dotnet restore "App.Api/App.Api.csproj"

COPY . .
WORKDIR "/src/App.Api"
RUN dotnet publish "App.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published API
COPY --from=build /app/publish .

# Copy appsettings files into the container (use env vars or secrets to override in production)
COPY App.Api/appsettings*.json ./

# Configure runtime
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "App.Api.dll"]