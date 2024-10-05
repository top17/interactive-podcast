namespace InteractivePodcast.Data.Entities
{
    public class ListeningHistory
    {
        public int Id { get; set; }
        public DateTime StartedListeningAt { get; set; }
        public DateTime? FinishedListeningAt { get; set; }
        public int Progress { get; set; }

        public Guid UserId { get; set; }
        public int PodcastId { get; set; }
    }
}
