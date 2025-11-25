# AppTemplate · .NET 8 Clean Architecture + Blazor WebAssembly

A production-ready reference solution that combines a **Clean Architecture back end** (CQRS, MediatR, EF Core, JWT with refresh tokens) with a **Blazor WebAssembly front end** styled with MudBlazor. The template covers authentication, todo task management, domain-driven abstractions, centralized error handling, and Docker-based deployment so you can bootstrap new projects quickly.

---

## Table of Contents
1. [Solution at a Glance](#solution-at-a-glance)
2. [Technology Stack](#technology-stack)
3. [Solution Structure](#solution-structure)
4. [Back-End Architecture](#back-end-architecture)
5. [Authentication & Authorization Flow](#authentication--authorization-flow)
6. [Front-End Application](#front-end-application)
7. [Local Development Setup](#local-development-setup)
8. [Database & Migrations](#database--migrations)
9. [Running the Applications](#running-the-applications)
10. [Containerized Deployment](#containerized-deployment)
11. [Testing & Quality](#testing--quality)
12. [Configuration Reference](#configuration-reference)
13. [Troubleshooting & Tips](#troubleshooting--tips)
---

## Solution at a Glance
- **Clean separation of concerns** across `Domain`, `Application`, `Infrastructure`, `API`, and `Frontend` projects.
- **CQRS with MediatR** for clear command/query separation and pipeline extensibility (validation, authorization).
- **JWT-based authentication** with refresh token rotation, revocation, and role claims (Admin/User).
- **Todo management** end-to-end: create, update, complete, re-open, and delete tasks through API and Blazor UI.
- **Production-friendly tooling**: global exception middleware, FluentValidation, AutoMapper, Swagger, Docker, and xUnit tests.

---

## Technology Stack
**Back End**
- .NET 8 Web API (`App.Api`)
- MediatR for CQRS request handling
- Entity Framework Core + SQL Server (Pomelo MySQL provider also referenced)
- FluentValidation for input rules
- AutoMapper for DTO <-> entity mapping
- BCrypt.Net for password hashing
- JWT Bearer Authentication with refresh tokens

**Front End**
- Blazor WebAssembly (`Todo.Frontend`)
- MudBlazor component library
- Tailwind
- Local storage for token persistence
- HttpClient with JWT bearer injection

**Tooling & Operations**
- Docker & docker-compose (API + SQL Server)
- dotnet-ef migrations

---

## Solution Structure
```
ToDo
├───Backend
│   ├───ToDo.Api  // ASP.NET Core entry point, controllers, middleware, DI
│   │   ├───Controllers
│   │   ├───Middleware
│   │   └───Properties
│   ├───ToDo.Application  // CQRS commands, queries, handlers, DTOs, behaviors
│   │   ├───Behaviors
│   │   ├───Commands
│   │   │   ├───TodoItems
│   │   │   │   ├───AddTodoItem
│   │   │   │   ├───DeleteTodoItem
│   │   │   │   └───UpdateTodoItem
│   │   │   ├───TokenRefreshRequest
│   │   │   │   ├───RevokeRefreshToken
│   │   │   │   └───TokenRefresh
│   │   │   └───Users
│   │   │       ├───AddUser
│   │   │       ├───DeleteUser
│   │   │       ├───LoginUser
│   │   │       └───UpdateUser
│   │   ├───DependencyInjection
│   │   ├───DTOs
│   │   ├───Interfaces
│   │   │   ├───Authentication
│   │   │   └───Authorizable
│   │   ├───Mappings
│   │   ├───Queries
│   │   │   ├───TodoItems
│   │   │   │   └───GetTodoItem
│   │   │   │       ├───ById
│   │   │   │       └───ByUserId
│   │   │   └───User
│   │   │       └───GetUser
│   │   │           ├───ByEmail
│   │   │           ├───ById
│   │   │           └───ByUsername
│   │   ├───Responses
│   │   └───Validators
│   │       ├───Exceptions
│   │       ├───Extensions
│   │       └───ValidationMessages
│   ├───ToDo.Domain  // Entities (User, TodoItem, RefreshToken) + repository abstractions
│   │   ├───Abstractions
│   │   └───Entities
│   └───ToDo.Infrastructure // EF Core DbContext, repositories, JWT, CORS, Swagger, DI
│       ├───DependencyInjection
│       └───Persistence
│           ├───Data
│           │   ├───DbContexts
│           │   │   └───Configurations
│           │   └───Migrations
│           └───Repositories
└───Frontend
    └───Todo.Frontend  // Blazor WebAssembly client using MudBlazor
        ├───Docker
        ├───Layout
        ├───Models
        ├───Pages
        ├───Properties
        └───wwwroot
```

---

## Back-End Architecture
### API Layer (`App.Api`)
- Controllers (`UsersController`, `AuthController`, `TodoItemsController`) translate HTTP requests into MediatR commands/queries.
- `ExceptionHandlingMiddleware` normalizes error responses, mapping `Unauthorized`, `Forbidden`, `NotFound`, and FluentValidation failures to JSON payloads.
- Swagger exposed automatically in Development for interactive API docs.
- HTTPS enforced by default; CORS policy `AllowAllOrigins` is registered via infrastructure configuration.

### Application Layer (`App.Application`)
- **Commands**: add/update/delete todo items, create/update/delete users, login, refresh/revoke tokens.
- **Queries**: fetch todo items (by ID, by user, by current user), fetch users (by ID, email, username).
- **Pipeline behaviors**:
  - `ValidationBehavior` applies all registered `IValidator<TRequest>` instances before handlers run and logs failures.
  - `AuthorizationBehavior` (available for requests implementing `IAuthorizableRequest`) guards resource access, supporting admin overrides.
- **Mappings**: AutoMapper profiles convert between commands, DTOs, and domain entities; password hashing handled centrally in the handler.
- **DTOs & Responses**: encapsulate data returned to API clients, keeping domain entities internal.

### Domain Layer (`App.Domain`)
- Entities:
  - `User` – includes email, hashed password, registration/login timestamps, `IsActive`, `IsAdmin`, and refresh token collection.
  - `TodoItem` – stores title, description, ownership (`UserId`), timestamps, and completion state.
  - `RefreshToken` – tracks issued refresh tokens with expiry and revocation flags.
- Repository abstractions (`IUserRepository`, `ITodoItemRepository`, `IRefreshTokenRepository`, `IUnitOfWork`) define persistence contracts consumed by the application layer.

### Infrastructure Layer (`App.Infrastructure`)
- `AppDbContext` (EF Core) exposes `DbSet<T>` for all domain entities and applies entity configurations automatically.
- Repository implementations translate abstractions into EF Core operations.
- Service registrations:
  - `AddDatabaseServices` configures EF Core against SQL Server/MySQL based on connection string.
  - `AddJwtServices` wires JwtBearer authentication, validation parameters, and diagnostic logging.
  - `AddCorsServices` registers the permissive CORS policy used by the API.
  - `AddSwaggerServices` prepares Swagger generation and UI.
- `JwtTokenGenerator` issues both access and refresh tokens with issuer/audience pulled from configuration.

---

## Authentication & Authorization Flow
1. **User registration** (`POST /api/users`) hashes the password and persists a new `User`.
2. **Login** (`POST /api/users/login`) checks credentials, issues an access token (1 hour) and a refresh token (7 days), and stores the latter persistently.
3. **Authenticated requests** carry the `Authorization: Bearer <accessToken>` header; the server validates issuer, audience, lifetime (`ClockSkew = 0`), and signature.
4. **Refresh** (`POST /api/auth/refresh`) validates the refresh token, rotates it, and returns a brand new pair.
5. **Revocation** (`POST /api/auth/revoked`) permanently invalidates a refresh token (e.g., on logout).
6. **Resource authorization** is handled at controller level via `[Authorize]` plus optional pipeline checks comparing request ownership (`IAuthorizableRequest`) with JWT claims.

---

## Front-End Application
- Built with **Blazor WebAssembly** (`Todo.Frontend`).
- `Program.cs` configures MudBlazor services and creates a scoped `HttpClient` pointed at `https://localhost:7003/` by default (align with API HTTPS port).
- Pages:
  - `Hero.razor` – landing/marketing section.
  - `Login.razor` – authenticates users, stores `accessToken`, `refreshToken`, and `username` in `localStorage`.
  - `Register.razor` – client-side validation + API call to create accounts.
  - `Dashboard.razor` – fetches current user tasks (`GET /api/TodoItems/me`), allows mark complete/uncomplete, inline editing, and deletes via API.
- UX details:
  - MudBlazor `ISnackbar` surfaces validation and error messages.
  - Long-running operations show MudBlazor progress indicators.
  - All mutating requests attach the bearer token via `Authorization` header.

---

## Local Development Setup
### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server 2022 (local instance) **or** Docker Desktop to run the bundled SQL Server container
- Optional: MySQL 8 if you prefer the Pomelo provider
- Node.js 18+ (required by MudBlazor build tooling for some workloads)
- `dotnet-ef` tool (install via `dotnet tool install --global dotnet-ef`)

### Clone & Restore
```bash
git clone <repository-url>
cd AppTemplate
dotnet restore
```

### Configure Environment
## Just add to your Environment:
      -ConnectionStrings__DefaultConnection:{DB_URL} 
      -Jwt__Key:{JWT_KEY} you can generate: https://jwtsecrets.com
      -Jwt_RefreshTokenKey:{JWT_REFRESHTOKEN} you can generate: https://jwtsecrets.com
---

## Database & Migrations
The solution ships with EF Core migrations under `App.Infrastructure/Persistence/Data/Migrations`.

```bash
# from repository root
cd App.Api

# Apply latest migrations (uses DefaultConnection)
dotnet ef database update

# Create a new migration (if you change the model)
dotnet ef migrations add <MigrationName> --project ../App.Infrastructure --startup-project .
```

> **Tip:** If you use Docker SQL Server, ensure the connection string points to `Server=localhost,1433;` or the Docker service name when running inside containers (`Server=database;` as provided in docker-compose).

---

## Running the Applications
### Back End (API)
```bash
cd App.Api
dotnet run --launch-profile https   # exposes https://localhost:7003 and http://localhost:5248
```
- Swagger UI: `https://localhost:7003/swagger`
- Global exception middleware returns structured error objects on failure.
- HTTPS developer certificate: run `dotnet dev-certs https --trust` once if prompted.

### Front End (Blazor WASM)
```bash
cd Todo.Frontend
dotnet run --launch-profile https   # defaults to https://localhost:7076
```
- The WASM client expects the API at `https://localhost:7003/`; adjust base address if needed.
- Tokens are read from `localStorage`. Clearing browser storage logs the user out.

### Creating Your First User
1. Navigate to the Blazor app at `/register` and create an account, **or** call `POST /api/users`.
2. Log in via `/login` or `POST /api/users/login` to obtain tokens.
3. Visit `/dashboard` to manage your todo items.

---

## Containerized Deployment
The `Docker/docker-compose.yml` file provisions:
- `api` service (multi-stage build using `Docker/Dockerfile`)
- `database` service (SQL Server 2022)

```bash
cd Docker
docker-compose up --build          # builds API, starts SQL Server
docker-compose up -d               # run detached
docker-compose down                # stop and remove containers
```

> The compose file builds only the API container. To deploy the Blazor WASM client together, host it from a static file server (e.g., Azure Static Web Apps, S3 + CloudFront) or add another container running `dotnet publish -c Release`.

---

## Testing & Quality

### Manual API QA
- Swagger UI provides interactive docs with JWT “Authorize” support.
- Postman/Insomnia collections can be created from the documented endpoints.
- Use `curl` snippets from the README for quick smoke tests.

### Observability
- Logging is provided via the built-in ASP.NET Core logging pipeline.
- JWT authentication events log success/failure for easier diagnostics (`JwtServiceRegistration`).

```
### Launch Profiles
- API: `https://localhost:7003` (HTTPS), `http://localhost:5248` (HTTP)
- Frontend: `https://localhost:7076` (HTTPS), `http://localhost:5206` (HTTP)

---

## Troubleshooting & Tips
- **CORS errors**: ensure the API is running and the frontend base address matches the API host. By default `AllowAllOrigins` is enabled.
- **SQL connectivity**: if using Docker for the API only, but local SQL Server, adjust connection string host to `host.docker.internal`.
- **Migrations during docker-compose**: run `dotnet ef database update` locally before starting containers, or bake migrations into the entrypoint of the API container.
- **Invalid token errors**: confirm `Jwt` settings match between API configuration and any external client.
- **HTTPS certificates**: trust the dev certificate (`dotnet dev-certs https --trust`) to avoid browser warnings for the API.

---

**Happy building!** This template is designed to be a solid foundation for production-ready .NET applications with a modern Blazor front end. Adapt, extend, and ship. 🚀
