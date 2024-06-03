
using System.Data;
using System.Reflection;
using eComWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eComWebApp.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly);
    }

}
