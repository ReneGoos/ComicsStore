namespace StoreFront.Model
{
    public class ArtistModel : InputModel
    {
        public ArtistModel() : base()
        {
            Stories = new StoryList();
        }

        public StoryList Stories { get; }
    }
}