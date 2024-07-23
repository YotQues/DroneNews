using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneNews.Model.Entities;

[Table("Authors")]
[PrimaryKey(nameof(Id))]
[Index(nameof(Name), IsUnique = true)]
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
