namespace ComicsLibrary.EditModels
{
    public class StoryOriginEditModel : CrossEditModel
    {
        private int? _storyId;
        private int? _originStoryId;

        public int? OriginStoryId { get => _originStoryId; set => SetIfValue(ref _originStoryId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }

        public override int? MainId { get => StoryId; set => StoryId = value; }
        public override int? LinkedId { get => OriginStoryId; set => OriginStoryId = value; }
    }
}