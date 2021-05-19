namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookPublisherInputModel : BasicCrossInputModel
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
    }
}
