using Microsoft.EntityFrameworkCore;

namespace JesonApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserResource> Users { get; set; } = null!;
        public DbSet<LyricResource> Lyrics { get; set; } = null!;
        public DbSet<FrameResource> Frames { get; set; } = null!;

        // ⚡ tady správně přepisujeme OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LyricResource>()
                .HasMany(l => l.Frames)
                .WithOne(f => f.LyricResource)
                .HasForeignKey(f => f.LyricResourceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}