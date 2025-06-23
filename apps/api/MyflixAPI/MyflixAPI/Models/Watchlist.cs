using System;
using System.Collections.Generic;

namespace MyflixAPI.Models;

public partial class Watchlist
{
    public string WatchlistUserIdRef { get; set; } = null!;

    public int WatchlistMovieIdRef { get; set; }

    public virtual Movie WatchlistMovieIdRefNavigation { get; set; } = null!;
}
