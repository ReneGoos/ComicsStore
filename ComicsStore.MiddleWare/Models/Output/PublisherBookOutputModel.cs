namespace ComicsStore.MiddleWare.Models.Output
{
    public class PublisherBookOutputModel : IPublisherBookOutputModel
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }

        public BookOnlyOutputModel Book { get; set; }
    }
}