using Application.Activities.Queries;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Register and configure the Entity Framework Core Database Context.
// 'AddDbContext<AppDbContext>' registers your AppDbContext in the Dependency Injection (DI) container.
// This allows you to inject the database context into your controllers or handlers.
//
// The lambda 'opt => ...' is used to set up the DB options:
//   - 'opt.UseSqlite()' tells EF Core to use the SQLite database provider.
//   - 'builder.Configuration.GetConnectionString("DefaultConnection")' looks for a section named 
//     "ConnectionStrings" in your appsettings.json and retrieves the "DefaultConnection" value
//     (in this case, "Data source=reactivities.db").
builder.Services.AddDbContext<AppDbContext>( opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
} );
// Register Cross-Origin Resource Sharing (CORS) services.
// CORS is a security mechanism that allows or restricts requested resources on a web page 
// to be requested from another domain outside the domain from which the first resource was served.
// This is essential for allowing your React frontend (localhost:3000) to call this API (localhost:5001).
builder.Services.AddCors();

// Configure MediatR for the application.
// 'builder.Services' refers to the collection of services in the ASP.NET Core Dependency Injection (DI) container.
// This is where you register all the components and services your application will use.
//
// 'AddMediatR()' is an extension method provided by the MediatR library.
// It registers the core MediatR services (like IMediator) into the DI container,
// making them available for injection throughout your application.
//
// The lambda expression 'x => ...' is used to configure MediatR's options.
// 'x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>()' is a crucial part:
//   - It instructs MediatR to scan the assembly (project) that contains the specified type,
//     which in this case is 'GetActivityList.Handler'.
//   - By doing so, MediatR automatically discovers and registers all classes that implement
//     'IRequestHandler<TRequest, TResponse>' (for commands/queries) and 'INotificationHandler<TNotification>'
//     (for notifications) within that assembly.
//   - 'GetActivityList.Handler' is chosen as a marker because it resides in the 'Application' project,
//     which is where all your business logic handlers (queries and commands) are located.
//     This ensures that all your MediatR handlers are correctly registered for dependency injection.
builder.Services.AddMediatR(x => 
    x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>());
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:3000", "https://localhost:3000"));

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
