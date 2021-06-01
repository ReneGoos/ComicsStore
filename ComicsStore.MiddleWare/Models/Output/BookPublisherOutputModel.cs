namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookPublisherOutputModel : PublisherOnlyOutputModel, IBookPublisherOutputModel
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
    }
}