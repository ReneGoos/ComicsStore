namespace ComicsStore.MiddleWare.Models.Output
{
    public class BookPublisherOutputModel : BasicCrossOutputModel
    {
        public int PublisherId { get; set; }

        public PublisherOnlyOutputModel Publisher { get; set; }
    }
}