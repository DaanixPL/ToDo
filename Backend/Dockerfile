FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files z Backend/
COPY ["Backend/ToDo.Api/ToDo.Api.csproj", "Backend/ToDo.Api/"]
COPY ["Backend/ToDo.Application/ToDo.Application.csproj", "Backend/ToDo.Application/"]
COPY ["Backend/ToDo.Domain/ToDo.Domain.csproj", "Backend/ToDo.Domain/"]
COPY ["Backend/ToDo.Infrastructure/ToDo.Infrastructure.csproj", "Backend/ToDo.Infrastructure/"]

# Restore
RUN dotnet restore "Backend/ToDo.Api/ToDo.Api.csproj"

# Copy cały folder Backend
COPY Backend/ Backend/

# Debug
WORKDIR "/src/Backend/ToDo.Api"

# Build and publish
RUN dotnet publish "ToDo.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "ToDo.Api.dll"]