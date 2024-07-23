using System.Runtime.Serialization;

namespace DroneNews.Contract.Dto;

public class ArticleDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string OriginalUrl { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime PublishedAt { get; set; }

    public string? Author { get; set; }

    public SourceDto? Source { get; set; }
}

