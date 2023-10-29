using ComicsLibrary.EditModels.Interfaces;

namespace ComicsLibrary.EditModels
{
    public class StoryOriginEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _storyId;
        private int? _originStoryId;
        private StoryOnlyEditModel _storyFromOrigin;

        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public int? OriginStoryId { get => _originStoryId; set => SetIfValue(ref _originStoryId, value); }

        public StoryOnlyEditModel StoryFromOrigin { get => _storyFromOrigin; set => SetIfValue(ref _storyFromOrigin, value); }

        public int? MainId { get => OriginStoryId; set => OriginStoryId = value; }
        public int? LinkedId { get => StoryId; set => StoryId = value; }
        public TableEditModel ChildItem { get => StoryFromOrigin; set => StoryFromOrigin = value as StoryOnlyEditModel; }
    }
}