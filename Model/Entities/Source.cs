using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneNews.Model.Entities;

[Table("Sources")]
[PrimaryKey(nameof(Id))]
[Index(nameof(Url), IsUnique = true)]
public class Source
{
    public int Id { get; set; }
    [Required]
    public required string Url { get; set; }
    public virtual ICollection<Article> Articles { get; } = new List<Article>();
}
