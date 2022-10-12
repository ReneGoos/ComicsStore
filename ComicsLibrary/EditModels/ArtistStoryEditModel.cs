namespace ComicsLibrary.EditModels
{
    public class ArtistStoryEditModel : StoryArtistEditModel
    {
        public override int? MainId { get => ArtistId; set => ArtistId = value; }
        public override int? LinkedId { get => StoryId; set => StoryId = value; }
    }
}