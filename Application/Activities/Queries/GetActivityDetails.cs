using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries;

public class GetActivityDetails
{
    public class Query : IRequest<Activity>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Activity>
    {
        public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.Id], cancellationToken);

            // We throw an exception if the activity is not found
            // We cannot return NotFound() here because we are in the Application layer, which should not have any dependencies on ASP.NET Core or its MVC framework. Instead, we throw an exception and let the global error handler take care of it.
            if (activity == null) throw new Exception("Activity not found");

            return activity;
        }
    }
}
