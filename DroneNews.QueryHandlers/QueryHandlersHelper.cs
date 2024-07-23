using Microsoft.Extensions.DependencyInjection;

namespace DroneNews.QueryHandlers;

public static class QueryHandlersHelper
{
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        Type iCommandHandler = typeof(QueryHandlerAttribute);

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.IsDefined(iCommandHandler, false));

        foreach (var type in types)
        {
            services.AddScoped(type, type);
        }
        return services;
    }
}
