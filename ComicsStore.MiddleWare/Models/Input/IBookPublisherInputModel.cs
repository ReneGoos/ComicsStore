namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IBookPublisherInputModel : IBasicCrossInputModel
    {
        int BookId { get; set; }
        int PublisherId { get; set; }
    }
}