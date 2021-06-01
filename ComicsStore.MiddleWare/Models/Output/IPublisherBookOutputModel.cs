namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IPublisherBookOutputModel
    {
        int BookId { get; set; }
        int PublisherId { get; set; }
    }
}