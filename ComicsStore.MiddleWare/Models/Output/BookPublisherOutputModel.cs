namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookPublisherOutputModel : PublisherOnlyOutputModel
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
    }
}