using System;
using System.Collections.Generic;

namespace MyflixAPI.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewCreatedAt { get; set; }

    public string? ReviewUserIdRef { get; set; }

    public int? ReviewMovieIdRef { get; set; }

    public int? ReviewParentReviewIdRef { get; set; }

    public virtual ICollection<Review> InverseReviewParentReviewIdRefNavigation { get; set; } = new List<Review>();

    public virtual Movie? ReviewMovieIdRefNavigation { get; set; }

    public virtual Review? ReviewParentReviewIdRefNavigation { get; set; }
}
