namespace ComicsStore.Data.Model
{
    public class Pseudonym : CrossTable
    {
        public Pseudonym() : base()
        {
        }

        public int MainArtistId { get; set; }
        public int PseudonymArtistId { get; set; }

        public Artist MainArtist { get; set; }
        public Artist PseudonymArtist { get; set; }
    }
}