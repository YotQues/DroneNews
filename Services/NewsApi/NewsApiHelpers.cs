using DroneNews.Services.NewsApi;
using Microsoft.Extensions.DependencyInjection;
using NewsAPI;

public static class NewsApiHelpers
{
    public static IServiceCollection AddNewsAPI(this IServiceCollection services, string apiKey)
    {
        services.AddScoped((services) => new NewsApiClient(apiKey));
        services.AddScoped(services => new NewsApi(apiKey));


        return services;
    }
}