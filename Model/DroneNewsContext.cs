using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model;

public class DroneNewsContext : DbContext
{
    public DbSet<Source> Sources { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Article> Articles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Source>();
        modelBuilder.Entity<Author>();
        modelBuilder.Entity<Article>();
    }
}
