using ComicsLibrary.EditModels.Interfaces;

namespace ComicsLibrary.EditModels
{
    public class StoryCodeEditModel : BasicEditModel, ICrossEditModel
    {
        private int? _storyId;
        private int? _codeId;
        private CodeOnlyEditModel _code;

        public int? CodeId { get => _codeId; set => SetIfValue(ref _codeId, value); }
        public int? StoryId { get => _storyId; set => SetIfValue(ref _storyId, value); }
        public CodeOnlyEditModel Code { get => _code; set => SetIfValue(ref _code, value); }

        public int? MainId { get => StoryId; set => StoryId = value; }
        public int? LinkedId { get => CodeId; set => CodeId = value; }
        public TableEditModel ChildItem { get => Code; set => Code = value as CodeOnlyEditModel; }
    }
}