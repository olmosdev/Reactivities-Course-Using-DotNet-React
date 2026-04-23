using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>( opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

/* 
   AppDbContext is typically registered as a 'Scoped' service (one instance per HTTP request).
   Since we are at application startup and there is no active HTTP request, we must manually 
   create a scope to resolve the database context from the Dependency Injection container.
   The 'using' keyword ensures that the scope and DB connection are properly disposed of 
   once the initialization is complete.
*/
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // Retrieve the database context from the container
    var context = services.GetRequiredService<AppDbContext>();
    // Apply any pending migrations and create the DB if it doesn't exist
    await context.Database.MigrateAsync();
    // Populate the database with initial seed data
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    // Log any errors that occur during the database setup process
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration.");
}

app.Run();
