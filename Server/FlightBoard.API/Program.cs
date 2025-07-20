using FlightBoard.Application;
using FlightBoard.Infrastructure;
using FlightBoard.Infrastructure.Data;
using FlightBoard.Infrastructure.Hubs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                    ?? new[] { "http://localhost:5173" };

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
PrintStartupBanner(logger, app.Environment);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapHub<FlightBoardHub>("/flightBoardhub");

logger.LogInformation("Initializing database...");
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FlightBoardDbContext>();
    context.Database.EnsureCreated();
    logger.LogInformation("Database initialized successfully");
}

app.Run();

static void PrintStartupBanner(ILogger<Program> logger, IWebHostEnvironment environment)
{
    logger.LogInformation("╔══════════════════════════════════════╗");
    logger.LogInformation("║            FlightBoard API           ║");
    logger.LogInformation("║        Flight Management System      ║");
    logger.LogInformation("╚══════════════════════════════════════╝");
    logger.LogInformation("Environment: {Environment}", environment.EnvironmentName);
    logger.LogInformation("Started at: {StartTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    logger.LogInformation("Logs directory: logs/");
}
