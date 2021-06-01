namespace ComicsStore.MiddleWare.Models.Input
{
    public class BookPublisherInputModel : BasicCrossInputModel, IBookPublisherInputModel
    {
        public int BookId { get; set; }
        public int PublisherId { get; set; }
    }
}
