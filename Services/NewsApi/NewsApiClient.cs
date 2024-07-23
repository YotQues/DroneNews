using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;

namespace DroneNews.Services.NewsApi;

public class NewsApi
{
    //public NewsApiClient client;
    //public NewsApi(NewsApiClient _client)
    //{
    //    client = _client;
    //}
    readonly string _apiKey;
    private readonly HttpClient http;
    public NewsApi(string apiKey)
    {
        _apiKey = apiKey;
        http = new(new HttpClientHandler
        {
            AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate)
        });
        http.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
        http.DefaultRequestHeaders.Add("x-api-key", _apiKey);
    }


    public async Task<NewsAPI.Models.ArticlesResult> GetEverythingAsync(EverythingRequest request)
    {
        var baseAddress = new Uri($"https://newsapi.org/v2/everything?apiKey={_apiKey}&q=drone&&sortBy=publishedAt&language=en&searchIn=title,description");
        var (page, pageSize, from, to) = request;

        var queryParameters = new List<string>();

        if (page != null)
        {
            queryParameters.Add($"page={page}");
        }
        if (pageSize != null)
        {
            queryParameters.Add($"pageSize={pageSize}");
        }
        if (from != null)
        {
            queryParameters.Add($"from={from?.ToString("s")}");
        }
        if (to != null)
        {
            queryParameters.Add($"to={to?.ToString("s")}");
        }

        var query = string.Join("&", queryParameters);
        var newUri = new Uri(baseAddress + "&" + query);

        //var response = await http.GetAsync(newUri);
        ArticlesResult articlesResult = new ArticlesResult();
        //HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, BASE_URL + endpoint + "?" + querystring);
        string? json = await ((await http.GetAsync(newUri)).Content?.ReadAsStringAsync());

        if (!string.IsNullOrWhiteSpace(json))
        {
            var parseSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new CustomEnumConverter<ErrorCodes>() }
            };

            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json, parseSettings);
            articlesResult.Status = apiResponse.Status;
            if (articlesResult.Status == Statuses.Ok)
            {
                articlesResult.TotalResults = apiResponse.TotalResults;
                articlesResult.Articles = apiResponse.Articles;
            }
            else
            {
                ErrorCodes errorCode = ErrorCodes.UnknownError;
                try
                {
                    errorCode = apiResponse.Code.Value;
                }
                catch (Exception)
                {
                    Debug.WriteLine("The API returned an error code that wasn't expected: " + apiResponse.Code.ToString());
                }

                articlesResult.Error = new Error
                {
                    Code = errorCode,
                    Message = apiResponse.Message
                };
            }
        }
        else
        {
            articlesResult.Status = Statuses.Error;
            articlesResult.Error = new Error
            {
                Code = ErrorCodes.UnexpectedError,
                Message = "The API returned an empty response. Are you connected to the internet?"
            };
        }

        return articlesResult;

    }

}

