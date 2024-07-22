using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System.Diagnostics;

namespace Model;

public class DroneNewsContext : DbContext
{
    #region Constructors
    public DroneNewsContext(DbContextOptions options) : base(options)
    {
        Debug.WriteLine($"{nameof(DroneNewsContext)} created");
    }
    #endregion Constructors

    #region Finalizers
    ~DroneNewsContext()
    {
        Debug.WriteLine($"{nameof(DroneNewsContext)} disposed");
    }
    #endregion Finalizers

    public virtual DbSet<Source> Sources { get; }
    public virtual DbSet<Author> Authors { get; }
    public virtual DbSet<Article> Articles { get; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Source>();
        modelBuilder.Entity<Author>();
        modelBuilder.Entity<Article>();
    }
}
