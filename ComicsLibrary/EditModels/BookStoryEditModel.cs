namespace ComicsLibrary.EditModels
{
    public class BookStoryEditModel : StoryBookEditModel
    {
        public override int? MainId { get => BookId; set => BookId = value; }
        public override int? LinkedId { get => StoryId; set => StoryId = value; }
    }
}