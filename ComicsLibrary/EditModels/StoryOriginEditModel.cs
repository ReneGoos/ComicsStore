namespace ComicsLibrary.EditModels
{
    public class StoryOriginEditModel : TableEditModel, ICrossEditModel
    {
        private int? _storyId;
        private int? _originStoryId;

        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public int? OriginStoryId { get => _originStoryId; set => SetIfValue(ref _originStoryId, value); }

        public StoryOnlyEditModel StoryFromOrigin { get; set; }

        public int? MainId { get => StoryId; set => StoryId = value; }
        public int? LinkedId { get => OriginStoryId; set => OriginStoryId = value; }
        public TableEditModel ChildItem { get => StoryFromOrigin; set => StoryFromOrigin = value as StoryOnlyEditModel; }
    }
}