namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookPublisherOutputModel : IBookPublisherOutputModel
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }

        public PublisherOnlyOutputModel Publisher { get; set; }
    }
}