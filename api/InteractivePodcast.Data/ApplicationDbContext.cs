using InteractivePodcast.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractivePodcast.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavoritePodcast> FavoritePodcasts { get; set; }
        public DbSet<PodcastReview> PodcastReviews { get; set; }
        public DbSet<ListeningHistory> ListeningHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoritePodcast>().HasKey(fp => new { fp.UserId, fp.PodcastId });

            modelBuilder
                .Entity<PodcastReview>()
                .HasOne<User>()
                .WithMany(u => u.PodcastReviews)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<PodcastReview>()
                .HasOne<Podcast>()
                .WithMany()
                .HasForeignKey(pr => pr.PodcastId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ListeningHistory>()
                .HasOne<User>()
                .WithMany(u => u.ListeningHistories)
                .HasForeignKey(lh => lh.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ListeningHistory>()
                .HasOne<Podcast>()
                .WithMany()
                .HasForeignKey(lh => lh.PodcastId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<FavoritePodcast>()
                .HasOne<User>()
                .WithMany(u => u.FavoritePodcasts)
                .HasForeignKey(fp => fp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<FavoritePodcast>()
                .HasOne<Podcast>()
                .WithMany()
                .HasForeignKey(fp => fp.PodcastId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
