using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest
    {
        public required Activity Activity { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities
                .FindAsync([request.Activity.Id], cancellationToken) 
            //if (activity == null) throw new Exception("Cannot find activity");
                    ?? throw new Exception("Cannot find activity");

            // We can update the Activity properties one by one
            // activity.Title = request.Activity.Title;

            // But we will use AutoMapper to map the properties from the request.Activity to the activity entity we fetched from the database. Install from NuGet AutoMapper by Jimmy Bogard
            // We need to set some configurations in Application\Core\MappingProfiles.cs and add it in API\Program.cs
            mapper.Map(request.Activity, activity);

            await context.SaveChangesAsync(cancellationToken);

        }
    }
}
