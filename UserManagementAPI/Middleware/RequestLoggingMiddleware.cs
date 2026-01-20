namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log incoming request
            var requestTime = DateTime.UtcNow;
            _logger.LogInformation(
                "Incoming Request: {Method} {Path} at {Time}",
                context.Request.Method,
                context.Request.Path,
                requestTime
            );

            // Continue processing the request
            await _next(context);

            // Log outgoing response
            _logger.LogInformation(
                "Outgoing Response: {Method} {Path} - Status: {StatusCode} - Duration: {Duration}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                (DateTime.UtcNow - requestTime).TotalMilliseconds
            );
        }
    }
}
