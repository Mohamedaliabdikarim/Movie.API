using Microsoft.EntityFrameworkCore;
using Movie.Core.Entities;

namespace Movie.Data.Data;

public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options)
    {
    }

    public DbSet<Movie.Core.Entities.Movie> Movies { get; set; } = null!;
    public DbSet<Actor> Actors { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Movie <-> Actor: mange-til-mange
        modelBuilder.Entity<Movie.Core.Entities.Movie>()
            .HasMany(m => m.Actors)
            .WithMany(a => a.Movies)
            .UsingEntity(j => j.ToTable("MovieActors"));

        // Movie -> Review: én-til-mange
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Movie)
            .WithMany(m => m.Reviews)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}