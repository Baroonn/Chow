using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new StoreConfiguration());
        modelBuilder.ApplyConfiguration(new MealComponentConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Order>? Orders { get; set; }
    public DbSet<Buyer>? Buyers { get; set; }
    public DbSet<Store>? Stores { get; set; }
    public DbSet<MealComponent>? MealComponents { get; set; }
}