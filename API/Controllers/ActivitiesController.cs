using System;
using Application.Activities.Commands;
using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// Using a Primary Constructor to inject the DataContext dependency into the controller
// https://localhost:5001/api/activities
public class ActivitiesController : BaseApiController
{
    [HttpGet]
    // To see how CancellationToken works, the first part is on GetActivityList.cs, where we can simulate a long-running operation with a loop and Task.Delay.
    // public async Task<ActionResult<List<Activity>>> GetActivities(CancellationToken ct)
    // {
    //     return await Mediator.Send(new GetActivityList.Query(), ct);
    // }
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await Mediator.Send(new GetActivityList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityDetail(string id)
    {
        return await Mediator.Send(new GetActivityDetails.Query { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateActivity(Activity activity)
    {
        return await Mediator.Send(new CreateActivity.Command { Activity = activity });
    }

    [HttpPut]
    public async Task<ActionResult> EditActivity(Activity activity)
    {
        await Mediator.Send(new EditActivity.Command { Activity = activity });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        await Mediator.Send(new DeleteActivity.Command { Id = id });

        return Ok();
    }

}
