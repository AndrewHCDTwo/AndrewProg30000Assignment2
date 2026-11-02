using AndrewProg30000Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace AndrewProg30000Assignment2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles { get; set; }
}