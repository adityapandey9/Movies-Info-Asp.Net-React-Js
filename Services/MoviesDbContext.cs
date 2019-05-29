using Microsoft.EntityFrameworkCore;
using moviesnet.Entities;

namespace moviesnet.Services
{
     public class MoviesDbContext : DbContext
     {
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               modelBuilder.Entity<MoivesActors>().HasKey(sc => new { sc.MoviesId, sc.ActorsId });
               
               modelBuilder.Entity<Producers>()
                .Property(p => p.DOB)
                .HasColumnType("datetime2");
               modelBuilder.Entity<Actors>()
                .Property(p => p.DOB)
                .HasColumnType("datetime2");
          }
          public DbSet<Movies> Movies { get; set; }
          public DbSet<Actors> Actors { get; set; }
          public DbSet<Producers> Producers { get; set; }
          public DbSet<MoivesActors> MoivesActors { get; set; }
          public MoviesDbContext(
               DbContextOptions<MoviesDbContext> options)
               : base(options)
          {
               // Database.EnsureDeleted();
               Database.EnsureCreated();
          }
     }
}