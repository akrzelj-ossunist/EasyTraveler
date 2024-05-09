using Microsoft.EntityFrameworkCore;
using ET.Core.Entities;


namespace ET.DataAccess.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Company { get; set; }
    public DbSet<Bus> Bus { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Route> Route { get; set; }
    public DbSet<Ticket> Ticket { get; set; }

}
