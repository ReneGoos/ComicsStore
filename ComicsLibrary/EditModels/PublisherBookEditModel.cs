namespace ComicsLibrary.EditModels
{
    public class PublisherBookEditModel : BookPublisherEditModel
    {
        public override int? MainId { get => PublisherId; set => PublisherId = value; }
        public override int? LinkedId { get => BookId; set => BookId = value; }
    }
}