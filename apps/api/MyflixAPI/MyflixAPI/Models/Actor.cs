using System;
using System.Collections.Generic;

namespace MyflixAPI.Models;

public partial class Actor
{
    public int ActorId { get; set; }

    public string ActorName { get; set; } = null!;

    public string? ActorBio { get; set; }

    public string? ActorProfilePath { get; set; }

    public string? ActorWiki { get; set; }

    public virtual ICollection<Movie> MaMovieIdRefs { get; set; } = new List<Movie>();
}
