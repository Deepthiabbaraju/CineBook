using Microsoft.EntityFrameworkCore;
using OnlineMovieTicketingApplication.Controllers;
using OnlineMovieTicketingApplication.Models;

namespace OnlineMovieTicketingApplication.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<Shows> Shows { get; set; }
        public DbSet<Seat> Seats { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>()
                .HasKey(s => new { s.TheatreId, s.ShowTime, s.Id });
            

            base.OnModelCreating(modelBuilder);
        }
    }
    
}
