using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DroneNews.Model.Config;

public static class AddDroneNewsContextExtension
{
    public static IServiceCollection AddDroneNewsContext(this IServiceCollection serviceProvider, IConfiguration config)
    {
        string connectionString = config.GetConnectionString("DroneNewsSqlServer") ?? throw new Exception("DroneNewsSqlServer Connection String not found");

        serviceProvider.AddDbContext<DroneNewsContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
        });

        return serviceProvider;
    }
}
