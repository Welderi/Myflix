using System;
using System.Collections.Generic;

namespace MyflixAPI.Models;

public partial class Rating
{
    public string RatingUserIdRef { get; set; } = null!;

    public int RatingMovieIdRef { get; set; }

    public int? RatingValue { get; set; }

    public virtual Movie RatingMovieIdRefNavigation { get; set; } = null!;
}
