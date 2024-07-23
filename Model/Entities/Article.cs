using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneNews.Model.Entities
{
    [Table("Articles")]
    [PrimaryKey(nameof(Id))]
    [Index(nameof(Title), IsUnique = true)]
    [Index(nameof(OriginalUrl), IsUnique = true)]
    public class Article
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string OriginalUrl { get; set; }
        public string? ImageUrl { get; set; }

        public string Content { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; }

        [ForeignKey(nameof(Author))]
        public int? AuthorId { get; set; }

        [ForeignKey(nameof(Source))]
        public int SourceId { get; set; }

        public virtual Author? Author { get; init; }
        public virtual Source Source { get; init; }
    }
}
