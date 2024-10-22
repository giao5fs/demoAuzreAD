using adminApp.Models;
using Microsoft.EntityFrameworkCore;

namespace adminApp.Data;

public class AdminDbContext : DbContext
{
    public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
    {

    }

    public DbSet<Timesheet> Timesheets { get; set; }
}