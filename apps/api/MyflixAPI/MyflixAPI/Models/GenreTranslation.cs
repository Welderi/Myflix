using System.ComponentModel.DataAnnotations.Schema;

namespace MyflixAPI.Models;

[Table("GenreTranslation")]
public partial class GenreTranslation
{
    public int? GtGenreIdRef { get; set; }

    public string? GtName { get; set; }

    public virtual Genre? GtGenreIdRefNavigation { get; set; }
}
