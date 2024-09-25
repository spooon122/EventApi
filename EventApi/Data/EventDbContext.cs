using EventApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EventApi.Data
{
    public class EventDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
            Database.EnsureCreated();
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                        .Property(e => e.CreatedDate)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
        }
    }

}
