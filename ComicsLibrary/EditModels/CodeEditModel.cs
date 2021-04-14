using System.Collections.Generic;

namespace ComicsLibrary.EditModels
{
    public class CodeEditModel : TableEditModel
    {
        private ICollection<SeriesCodeEditModel> _seriesCodes;
        private ICollection<StoryCodeEditModel> _storyCodes;

        public CodeEditModel() : base()
        {
            SeriesCode = new List<SeriesCodeEditModel>();
            StoryCode = new List<StoryCodeEditModel>();
        }

        public ICollection<SeriesCodeEditModel> SeriesCode { get => _seriesCodes; set { Set(ref _seriesCodes, value); } }
        public ICollection<StoryCodeEditModel> StoryCode { get => _storyCodes; set { Set(ref _storyCodes, value); } }
    }
}
