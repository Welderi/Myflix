using System.ComponentModel.DataAnnotations.Schema;

namespace MyflixAPI.Models;

[Table("ActorTranslation")]
public partial class ActorTranslation
{
    public int? AtActorIdRef { get; set; }

    public string? AtLanguage { get; set; }

    public string? AtName { get; set; }

    public string? AtBio { get; set; }

    public string? AtWiki { get; set; }

    public virtual Actor? ATActorIdRefNavigation { get; set; }
}
