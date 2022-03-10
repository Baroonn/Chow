using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StoreConfiguration());
        modelBuilder.ApplyConfiguration(new MealComponentConfiguration());
    }

    public DbSet<Order>? Orders { get; set; }
    public DbSet<Buyer>? Buyers { get; set; }
    public DbSet<Store>? Stores { get; set; }
    public DbSet<MealComponent>? MealComponents { get; set; }
}