using ComicsLibrary.EditModels.Interfaces;

namespace ComicsLibrary.EditModels
{
    public class CodeStoryEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _storyId;
        private int? _codeId;
        private StoryOnlyEditModel _story;

        public int? CodeId { get => _codeId; set => SetIfValue(ref _codeId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public StoryOnlyEditModel Story { get => _story; set => SetIfValue(ref _story, value); }

        public int? MainId { get => CodeId; set => CodeId = value; }
        public int? LinkedId { get => StoryId; set => StoryId = value; }
        public TableEditModel ChildItem { get => Story; set => Story = value as StoryOnlyEditModel; }
    }
}