# Frontend: publish Blazor WASM and serve with nginx (lightweight, supports SPA fallback)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Todo.Frontend/Todo.Frontend.csproj", "Todo.Frontend/"]
RUN dotnet restore "Todo.Frontend/Todo.Frontend.csproj"
COPY . .
WORKDIR "/src/Todo.Frontend"
RUN dotnet publish "Todo.Frontend.csproj" -c Release -o /app/publish

FROM nginx:stable-alpine AS final
# Replace nginx default config to provide SPA fallback
COPY Todo.Frontend/nginx.conf /etc/nginx/conf.d/default.conf
# Copy published static files
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]