using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

/// <summary>
/// The AppDbContext class acts as the primary gateway to the database.
/// It inherits from DbContext, which provides the API to perform CRUD operations.
/// Using C# 12 Primary Constructors to pass DbContextOptions to the base class.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Represents the Activities table in the database. 
    /// The 'required' keyword ensures this property is initialized, 
    /// helping to avoid null reference issues at runtime.
    /// </summary>
    public required DbSet<Activity> Activities { get; set; }
}
