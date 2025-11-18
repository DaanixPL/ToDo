FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["App.Api/App.Api.csproj", "Backend/App.Api/"]
COPY ["App.Application/App.Application.csproj", "Backend/App.Application/"]
COPY ["App.Domain/App.Domain.csproj", "Backend/App.Domain/"]
COPY ["App.Infrastructure/App.Infrastructure.csproj", "Backend/App.Infrastructure/"]
RUN dotnet restore "Backend/App.Api/App.Api.csproj"

COPY . .
WORKDIR "/src/Backend/App.Api"
RUN dotnet publish "App.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published API
COPY --from=build /app/publish .

COPY App.Api/appsettings*.json ./

# Configure runtime
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "App.Api.dll"]