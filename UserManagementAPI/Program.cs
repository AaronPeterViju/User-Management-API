using UserManagementAPI.Data;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Register UserRepository as a singleton
builder.Services.AddSingleton<UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline

// 1. Error-handling middleware FIRST (catches all exceptions)
app.UseMiddleware<ErrorHandlingMiddleware>();

// 2. Authentication middleware NEXT (validates tokens)
app.UseMiddleware<AuthenticationMiddleware>();

// 3. Logging middleware LAST (logs after auth, before controllers)
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map controllers
app.MapControllers();

// Health check endpoint (bypasses authentication)
app.MapGet("/health", () => new { status = "Healthy", timestamp = DateTime.UtcNow })
    .WithName("HealthCheck");

app.Run();
