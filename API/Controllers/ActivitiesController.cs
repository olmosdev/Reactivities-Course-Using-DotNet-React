using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

// Using a Primary Constructor to inject the DataContext dependency into the controller
// https://localhost:5001/api/activities
public class ActivitiesController(AppDbContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await context.Activities.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityDetail(string id)
    {
        var activity = await context.Activities.FindAsync(id);

        if (activity == null) return NotFound(); // NotFound() returns a 404 status code if the activity is not found. It's thanks to the use of ActionResult<T> that we can return either an Activity or a NotFound result from this method.

        return activity;
    }

}
