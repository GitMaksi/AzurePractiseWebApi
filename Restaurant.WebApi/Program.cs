using Restaurant.Application.Extensions;
using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;
using Restaurant.WebApi.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddApplication();

builder.Services.AddTransient<ErrorHandlingMiddleware>();
builder.Services.AddTransient<TimeTrackingMiddleware>();

//Serilog setup
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Override("Microsoft",  Serilog.Events.LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore",  Serilog.Events.LogEventLevel.Information)
        .WriteTo.File("Log/Restaurant/ .log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
        .WriteTo.Console();
});

var app = builder.Build();

// Data seeding for Restaurant Tests
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

//adding middlewares
app.UseMiddleware<ErrorHandlingMiddleware>()
    .UseMiddleware<TimeTrackingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Redirecting HTTP to HTTPS
app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

//Mapping controllers
app.MapControllers();

app.Run();