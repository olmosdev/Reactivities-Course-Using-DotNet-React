using System;

namespace Domain;

// Domain Entity
public class Activity
{
    // We need to make the attributes public so that Entity Framework can access them
    // We can use [Key] on this attribute but since we used the "Id" convention, Entity Framework already knows that this field will be the primary key
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public DateTime Date { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public bool IsCancelled { get; set; }

    // Location props
    public required string City { get; set; }
    public required string Venue { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
