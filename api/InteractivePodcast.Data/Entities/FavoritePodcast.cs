namespace InteractivePodcast.Data.Entities
{
    public class FavoritePodcast
    {
        public Guid UserId { get; set; }
        public int PodcastId { get; set; }
        public DateTime FavoritedAt { get; set; }
    }
}
