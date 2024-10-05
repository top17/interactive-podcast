using InteractivePodcast.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<FavoritePodcast> FavoritePodcasts { get; set; } =
        new List<FavoritePodcast>();
    public ICollection<PodcastReview> PodcastReviews { get; set; } = new List<PodcastReview>();
    public ICollection<ListeningHistory> ListeningHistories { get; set; } =
        new List<ListeningHistory>();
}
