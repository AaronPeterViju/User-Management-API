namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;
        private const string ValidToken = "Bearer my-secret-token-12345";

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip authentication for certain paths (e.g., health checks, swagger)
            if (context.Request.Path.StartsWithSegments("/health") ||
                context.Request.Path.StartsWithSegments("/openapi") ||
                context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            // Check for Authorization header
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                _logger.LogWarning("Request missing Authorization header");
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = "Unauthorized. Missing token." });
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].ToString();

            // Validate token
            if (authHeader != ValidToken)
            {
                _logger.LogWarning("Invalid token provided: {Token}", authHeader);
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = "Unauthorized. Invalid token." });
                return;
            }

            _logger.LogInformation("Request authenticated successfully");
            await _next(context);
        }
    }
}
