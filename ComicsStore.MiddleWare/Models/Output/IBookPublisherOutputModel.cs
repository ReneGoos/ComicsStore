namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IBookPublisherOutputModel
    {
        int BookId { get; set; }
        int PublisherId { get; set; }
    }
}