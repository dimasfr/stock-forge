using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

// [ApiController] turns on API-specific behavior:
//   - automatic HTTP 400 when model validation fails (Phase 8)
//   - automatic binding of [FromBody] / [FromQuery] sources
//   - ProblemDetails responses for errors
// NestJS comparison: @Controller() plus a global ValidationPipe in one attribute.
[ApiController]

// "[controller]" is a token replaced by the class name without the "Controller" suffix.
// HealthController -> "health", so the final route is /api/health.
[Route("api/[controller]")]

// ControllerBase is the base class for API controllers (no view support, unlike Controller).
// ASP.NET Core creates a NEW instance of this class for every request.
public class HealthController : ControllerBase
{
    // [HttpGet] maps HTTP GET /api/health to this method.
    // IActionResult lets us choose the HTTP status code (Ok = 200, NotFound = 404, etc.).
    // NestJS comparison: @Get() on a method, with return values serialized to JSON.
    [HttpGet]
    public IActionResult Get()
    {
        // An anonymous object, serialized to JSON by System.Text.Json.
        // Property names are converted to camelCase by default: { "status": "ok", ... }
        var response = new
        {
            status = "ok",
            timestampUtc = DateTime.UtcNow
        };

        return Ok(response);
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}