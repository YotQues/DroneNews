using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Model.Config;

internal class DesignTimeDroneNewsDbContextFactory: IDesignTimeDbContextFactory<DroneNewsContext>
{
    public DroneNewsContext CreateDbContext(string[] args)
    {

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();

        string connectionString = configuration.GetConnectionString("DroneNewsSqlServer") ?? throw new Exception("Connection string not found");

        DbContextOptionsBuilder optionsBuilder = new();
        optionsBuilder.UseSqlServer(connectionString);

        return new DroneNewsContext(optionsBuilder.Options);
    }
}
