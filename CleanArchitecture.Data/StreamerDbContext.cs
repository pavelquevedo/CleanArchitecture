using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS; 
                Initial Catalog=Streamer; Integrated Security=True")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //We can declare manually the foreign keys using FluentAPI
            //if we're not following the naming conventions suggested by EntityFramework
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Definition of many to many relationship
            modelBuilder.Entity<Video>()
                .HasMany(m => m.Actors)
                .WithMany(m => m.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );
        }

        public DbSet<Streamer> Streamers { get; set; }
        public DbSet<Video> Videos { get; set; } 
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
    }
}