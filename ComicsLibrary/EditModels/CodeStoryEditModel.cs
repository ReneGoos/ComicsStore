namespace ComicsLibrary.EditModels
{
    public class CodeStoryEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _storyId;
        private int? _codeId;

        public int? CodeId { get => _codeId; set => SetIfValue(ref _codeId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public StoryOnlyEditModel Story { get; set; }

        public int? MainId { get => CodeId; set => CodeId = value; }
        public int? LinkedId { get => StoryId; set => StoryId = value; }
        public TableEditModel ChildItem { get => Story; set => Story = value as StoryOnlyEditModel; }
    }
}