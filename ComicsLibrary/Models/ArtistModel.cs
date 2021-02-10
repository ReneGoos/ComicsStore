namespace ComicsLibrary.Models
{
    public class ArtistModel : InputModel
    {
        public ArtistModel() : base()
        {
            Stories = new ArtistStoryList();
        }

        public ArtistStoryList Stories { get; set; }
    }
}