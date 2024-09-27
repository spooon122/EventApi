using Microsoft.EntityFrameworkCore;
using Shared.Models.Models;
namespace EventApi.Data
{
    public class EventDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                        .Property(e => e.CreatedDate)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            modelBuilder.Entity<Subscription>()
                        .HasKey(s => new { s.UserId, s.EventId });


            modelBuilder.Entity<Subscription>()
                        .HasOne(s => s.Event)
                        .WithMany(e => e.Subscriptions)
                        .HasForeignKey(e => e.EventId);

        }
    }

}
