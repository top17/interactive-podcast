namespace InteractivePodcast.Data.Entities
{
    public class PodcastReview
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }

        public Guid UserId { get; set; }
        public int PodcastId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
