using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Activities.Queries;

// Logic to go out to our database and get the activities
// This file implements a CQRS query using MediatR to retrieve a list of activities from the database.
// It is part of the Application layer in Clean Architecture, separating business logic from infrastructure.
// The query is handled asynchronously to avoid blocking the main thread.

// The Query class represents the request for fetching all activities.
// It implements IRequest<List<Activity>> from MediatR, indicating it expects a List<Activity> as response.
// This class has no properties, as it's a simple "get all" request, but it could be extended with filters.
public class GetActivityList
{
    public class Query : IRequest<List<Activity>> { }

    // The Handler class processes the Query request.
    // It implements IRequestHandler<Query, List<Activity>>, registering it with MediatR.
    // The primary constructor injects AppDbContext (via Dependency Injection) for database access.
    public class Handler(AppDbContext context/*, ILogger<GetActivityList> logger*/) : IRequestHandler<Query, List<Activity>>
    {
        // The Handle method executes the query logic.
        // It receives the Query (unused here), a CancellationToken for async cancellation,
        // and returns a Task<List<Activity>>.
        // Uses EF Core to asynchronously fetch all activities from the database.
        public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {   
            // To see how CancellationToken works, we can simulate a long-running operation with a loop and Task.Delay.
            // The second part is on ActivitiesController.cs, where we can cancel the request from the client side.
            // try
            // {
            //     for (int i = 0; i < 10; i++)
            //     {
            //         cancellationToken.ThrowIfCancellationRequested(); // Check for cancellation before each iteration
            //         await Task.Delay(1000, cancellationToken); // Simulate a delay (e.g., long-running operation)
            //         logger.LogInformation($"Task {i + 1}/10 completed."); // Log progress
            //     }
            // }
            // catch (System.Exception)
            // {
                
            //     logger.LogInformation("Task was cancelled."); // Log cancellation
            // }
        
            return await context.Activities.ToListAsync(cancellationToken);
        }
    }
}
