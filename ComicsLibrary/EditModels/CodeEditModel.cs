using System.Collections.Generic;

namespace ComicsLibrary.EditModels
{
    public class CodeEditModel : TableEditModel
    {
        private ICollection<StoryCodeEditModel> _storyCodes;

        public CodeEditModel()
            : base()
        {
        }

        public ICollection<StoryCodeEditModel> StoryCode { get => _storyCodes; set { Set(ref _storyCodes, value); } }
    }
}
