// Program.cs has TWO phases, and the order matters:
//   1. "builder" phase: register services into the DI container (what exists).
//   2. "app" phase: build the app, then configure the middleware pipeline (what runs per request).
//
// NestJS comparison: phase 1 is like the `providers` array in modules,
// phase 2 is like `app.use(...)` / global guards and pipes in main.ts.

// CreateBuilder reads configuration (appsettings.json, appsettings.{Environment}.json,
// environment variables, command-line args), sets up logging, and prepares the Kestrel web server.
var builder = WebApplication.CreateBuilder(args);

// ---------- Phase 1: register services in the DI container ----------

// AddControllers scans the assembly for classes that inherit ControllerBase
// and registers the machinery that maps HTTP requests to them.
builder.Services.AddControllers();

// Generates an OpenAPI document (the machine-readable API description).
// Later the React frontend can generate typed API clients from it.
builder.Services.AddOpenApi();

// Build() freezes the service registrations. After this line you cannot add more services.
var app = builder.Build();

// ---------- Phase 2: configure the middleware pipeline ----------

// IsDevelopment() reads the ASPNETCORE_ENVIRONMENT variable (set in launchSettings.json).
// We only expose the OpenAPI document in Development, not in Production.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware runs in the order it is added. Each one can handle the request,
// or pass it to the next one. Here: redirect http -> https first.
app.UseHttpsRedirection();

// Authorization middleware. It does nothing useful until we add authentication and [Authorize] (Phase 6).
app.UseAuthorization();

// Connects the routes declared by [Route]/[HttpGet] attributes on controllers to the pipeline.
app.MapControllers();

// Starts Kestrel and blocks until the app is shut down.
app.Run();
