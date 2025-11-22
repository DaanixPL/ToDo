FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Todo.Frontend/Todo.Frontend.csproj", "Todo.Frontend/"]

RUN dotnet restore "Todo.Frontend/Todo.Frontend.csproj"

COPY Todo.Frontend/ Todo.Frontend/

WORKDIR "/src/Todo.Frontend"
RUN dotnet publish "Todo.Frontend.csproj" -c Release -o /app/publish

FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

RUN rm -rf ./*

COPY --from=build /app/publish/wwwroot .

RUN cat > /etc/nginx/nginx.conf <<'EOF'
events {
    worker_connections 1024;
}

http {
    include /etc/nginx/mime.types;
    default_type application/octet-stream;

    types {
        application/wasm wasm;
    }

    gzip on;
    gzip_vary on;
    gzip_proxied any;
    gzip_comp_level 6;
    gzip_types text/plain text/css text/xml text/javascript application/json application/javascript application/wasm application/xml+rss;

    server {
        listen 80;
        server_name _;

        root /usr/share/nginx/html;
        index index.html;

        location / {
            try_files $uri $uri/ /index.html =404;
        }

        location ~* \.(dll|pdb|wasm|dat)$ {
            add_header Cache-Control "public, max-age=31536000, immutable";
        }

        location ~* \.(js|css|png|jpg|jpeg|gif|ico|svg|woff|woff2|ttf|eot)$ {
            add_header Cache-Control "public, max-age=604800, immutable";
        }

        add_header X-Frame-Options "SAMEORIGIN" always;
        add_header X-Content-Type-Options "nosniff" always;
        add_header X-XSS-Protection "1; mode=block" always;
    }
}
EOF

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]