using DroneNews.Contract.Dto;
using DroneNews.Model.Entities;

namespace DroneNews.QueryHandlers;

public static class Map_Dto_Entity
{
    public static ArticleDto ToDto(this Article article) => new()
    {
        Id = article.Id,
        Title = article.Title,
        Description = article.Description,
        Content = article.Content,
        PublishedAt = article.PublishedAt,
        ImageUrl = article.ImageUrl,
        OriginalUrl = article.OriginalUrl,
        Author = article.Author?.Name,
        Source = article.Source?.ToDto()
    };


    public static SourceDto ToDto(this Source source) => new() { Id = source.Id, Url = source.Url };
    public static AuthorDto ToDto(this Author author) => new() { Id = author.Id, Name = author.Name };
}
