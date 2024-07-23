
namespace DroneNews.Services.NewsApi;

public class NewsApiHttpClientHandler : HttpClientHandler
{
    protected readonly string apiKey;

    public NewsApiHttpClientHandler(string apiKey)
    {
        this.apiKey = apiKey;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        
        if (request.RequestUri == null)
        {
            return base.SendAsync(request, cancellationToken);
        }
        string uri = request.RequestUri.ToString();
        string query = request.RequestUri.Query;
        string separator = string.IsNullOrEmpty(query) ? "?" : "&";

        request.RequestUri = new Uri(uri + $"{separator}apiKey={apiKey}");

        return base.SendAsync(request, cancellationToken);
    }
}
