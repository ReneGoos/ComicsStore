namespace ComicsLibrary.EditModels
{
    public class StoryCodeEditModel : CrossEditModel
    {
        private int? _storyId;
        private int? _codeId;

        public int? CodeId { get => _codeId; set => SetIfValue(ref _codeId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }

        public override int? MainId { get => StoryId; set => StoryId = value; }
        public override int? LinkedId { get => CodeId; set => CodeId = value; }
    }
}