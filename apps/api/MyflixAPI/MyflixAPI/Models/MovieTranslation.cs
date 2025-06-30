using System.ComponentModel.DataAnnotations.Schema;

namespace MyflixAPI.Models;

[Table("MovieTranslation")]
public partial class MovieTranslation
{
    public int? MtMovieIdRef { get; set; }

    public string? MtLanguage { get; set; }

    public string? MtTitle { get; set; }

    public string? MtOverview { get; set; }

    public virtual Movie? MTMovieIdRefNavigation { get; set; }
}
