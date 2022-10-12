using ComicsLibrary.Core;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class CodeEditModel : TableEditModel
    {
        private ObservableChangedCollection<CodeSeriesEditModel> _seriesCodes;
        private ObservableChangedCollection<CodeStoryEditModel> _storyCodes;

        public CodeEditModel() : base()
        {
            Series = new ObservableChangedCollection<CodeSeriesEditModel>();
            Story = new ObservableChangedCollection<CodeStoryEditModel>();
        }

        public ObservableChangedCollection<CodeSeriesEditModel> Series { get => _seriesCodes; set => Set(ref _seriesCodes, value); }
        public ObservableChangedCollection<CodeStoryEditModel> Story { get => _storyCodes; set => Set(ref _storyCodes, value); }

        public void AddSeriesCodes(int? seriesId, int? oldSeriesId)
        {
            Series.HandleItem(Id, seriesId, oldSeriesId);
        }

        public void AddStoryCodes(int? storyId, int? oldStoryId)
        {
            Story.HandleItem(Id, storyId, oldStoryId);
        }

        public override void ResetId()
        {
            Id = null;
        }
    }
}
