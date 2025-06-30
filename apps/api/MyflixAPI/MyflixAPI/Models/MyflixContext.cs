using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyflixAPI.Models;

public partial class MyflixContext : IdentityDbContext<ApplicationUser>
{
    public MyflixContext()
    {
    }

    public MyflixContext(DbContextOptions<MyflixContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Watchlist> Watchlists { get; set; }

    public virtual DbSet<ActorTranslation> ActorTranslations { get; set; }

    public virtual DbSet<MovieTranslation> MovieTranslations { get; set; }

    public virtual DbSet<GenreTranslation> GenreTranslations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("your fallback connection if needed");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__Actor__57B3EA4BC121430E");

            entity.ToTable("Actor");

            entity.Property(e => e.ActorName).HasMaxLength(100);
            entity.Property(e => e.ActorProfilePath).HasMaxLength(255);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__0385057E387D70EE");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreName).HasMaxLength(100);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movie__4BD2941AABF55F6E");

            entity.ToTable("Movie");

            entity.Property(e => e.MovieTitle).HasMaxLength(255);

            entity.HasMany(d => d.MaActorIdRefs).WithMany(p => p.MaMovieIdRefs)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieActor",
                    r => r.HasOne<Actor>().WithMany()
                        .HasForeignKey("MaActorIdRef")
                        .HasConstraintName("FK_MA_Actor"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MaMovieIdRef")
                        .HasConstraintName("FK_MA_Movie"),
                    j =>
                    {
                        j.HasKey("MaMovieIdRef", "MaActorIdRef").HasName("PK__MovieAct__B27DAB4B1B32CDDC");
                        j.ToTable("MovieActor");
                        j.IndexerProperty<int>("MaMovieIdRef").HasColumnName("MA_MovieIdRef");
                        j.IndexerProperty<int>("MaActorIdRef").HasColumnName("MA_ActorIdRef");
                    });

            entity.HasMany(d => d.MgGenreIdRefs).WithMany(p => p.MgMovieIdRefs)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("MgGenreIdRef")
                        .HasConstraintName("FK_MG_Genre"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MgMovieIdRef")
                        .HasConstraintName("FK_MG_Movie"),
                    j =>
                    {
                        j.HasKey("MgMovieIdRef", "MgGenreIdRef").HasName("PK__MovieGen__C3FDF31C0FF81787");
                        j.ToTable("MovieGenre");
                        j.IndexerProperty<int>("MgMovieIdRef").HasColumnName("MG_MovieIdRef");
                        j.IndexerProperty<int>("MgGenreIdRef").HasColumnName("MG_GenreIdRef");
                    });
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => new { e.RatingMovieIdRef, e.RatingUserIdRef }).HasName("PK__Rating__EC9E00C03E6B34BF");

            entity.ToTable("Rating");

            entity.Property(e => e.RatingMovieIdRef).HasColumnName("Rating_MovieIdRef");
            entity.Property(e => e.RatingUserIdRef).HasColumnName("Rating_UserIdRef");

            entity.HasOne(d => d.RatingMovieIdRefNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RatingMovieIdRef)
                .HasConstraintName("FK_Rating_Movie");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__74BC79CE430FFC6A");

            entity.ToTable("Review");

            entity.Property(e => e.ReviewCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReviewMovieIdRef).HasColumnName("Review_MovieIdRef");
            entity.Property(e => e.ReviewParentReviewIdRef).HasColumnName("Review_ParentReviewIdRef");
            entity.Property(e => e.ReviewUserIdRef)
                .HasMaxLength(450)
                .HasColumnName("Review_UserIdRef");

            entity.HasOne(d => d.ReviewMovieIdRefNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ReviewMovieIdRef)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Review_Movie");

            entity.HasOne(d => d.ReviewParentReviewIdRefNavigation).WithMany(p => p.InverseReviewParentReviewIdRefNavigation)
                .HasForeignKey(d => d.ReviewParentReviewIdRef)
                .HasConstraintName("FK_Review_Parent");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasKey(e => new { e.WatchlistUserIdRef, e.WatchlistMovieIdRef }).HasName("PK__Watchlis__17B5527A5A4C79F0");

            entity.ToTable("Watchlist");

            entity.Property(e => e.WatchlistUserIdRef).HasColumnName("Watchlist_UserIdRef");
            entity.Property(e => e.WatchlistMovieIdRef).HasColumnName("Watchlist_MovieIdRef");

            entity.HasOne(d => d.WatchlistMovieIdRefNavigation).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.WatchlistMovieIdRef)
                .HasConstraintName("FK_Watchlist_Movie");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasOne<IdentityUser>()
                  .WithMany()
                  .HasForeignKey(e => e.RatingUserIdRef)
                  .HasConstraintName("FK_Rating_User");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne<IdentityUser>()
                  .WithMany()
                  .HasForeignKey(e => e.ReviewUserIdRef)
                  .HasConstraintName("FK_Review_User");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasOne<IdentityUser>()
                  .WithMany()
                  .HasForeignKey(e => e.WatchlistUserIdRef)
                  .HasConstraintName("FK_Watchlist_User");
        });

        modelBuilder.Entity<MovieTranslation>(entity =>
        {
            entity.HasKey(e => new { e.MtMovieIdRef });

            entity.Property(e => e.MtLanguage)
                .HasMaxLength(10)
                .HasColumnName("MT_Language");
            entity.Property(e => e.MtMovieIdRef).HasColumnName("MT_MovieIdRef");
            entity.Property(e => e.MtOverview).HasColumnName("MT_Overview");
            entity.Property(e => e.MtTitle)
                .HasMaxLength(255)
                .HasColumnName("MT_Title");

            entity.HasOne(d => d.MTMovieIdRefNavigation).WithMany()
                .HasForeignKey(d => d.MtMovieIdRef)
                .HasConstraintName("FK_MT_Movie");
        });

        modelBuilder.Entity<ActorTranslation>(entity =>
        {
            entity.HasKey(e => new { e.AtActorIdRef, e.AtLanguage });

            entity.Property(e => e.AtActorIdRef).HasColumnName("AT_ActorIdRef");
            entity.Property(e => e.AtBio).HasColumnName("AT_Bio");
            entity.Property(e => e.AtLanguage)
                .HasMaxLength(10)
                .HasColumnName("AT_Language");
            entity.Property(e => e.AtName)
                .HasMaxLength(100)
                .HasColumnName("AT_Name");
            entity.Property(e => e.AtWiki).HasColumnName("AT_Wiki");

            entity.HasOne(d => d.ATActorIdRefNavigation).WithMany()
                .HasForeignKey(d => d.AtActorIdRef)
                .HasConstraintName("FK_AT_Actor");
        });

        modelBuilder.Entity<GenreTranslation>(entity =>
        {
            entity.HasKey(e => new { e.GtGenreIdRef });

            entity.Property(e => e.GtGenreIdRef).HasColumnName("GT_GenreIdRef");
            entity.Property(e => e.GtName)
                .HasMaxLength(100)
                .HasColumnName("GT_Name");

            entity.HasOne(d => d.GtGenreIdRefNavigation).WithMany()
                .HasForeignKey(d => d.GtGenreIdRef)
                .HasConstraintName("FK_GT_Genre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
