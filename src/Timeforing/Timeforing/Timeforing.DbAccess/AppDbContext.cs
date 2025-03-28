using Microsoft.EntityFrameworkCore;
using Timeforing.Model;

namespace Timeforing.DbAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();
}