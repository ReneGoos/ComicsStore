namespace ComicsLibrary.EditModels
{
    public class CodeStoryEditModel : StoryCodeEditModel
    {
        public override int? MainId { get => CodeId; set => CodeId = value; }
        public override int? LinkedId { get => StoryId; set => StoryId = value; }
    }
}