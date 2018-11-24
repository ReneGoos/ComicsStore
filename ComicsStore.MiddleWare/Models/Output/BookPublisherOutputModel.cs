namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookPublisherOutputModel
    {
        public int PublisherId { get; set; }

        public PublisherOnlyOutputModel Publisher { get; set; }
    }
}