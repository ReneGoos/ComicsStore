using ComicsLibrary.Core;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class CodeEditModel : CodeOnlyEditModel
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

        public bool HandleSeries(int? oldSeriesId, SeriesOnlyEditModel series)
        {
            return Series.HandleItem(Id, oldSeriesId, series);
        }

        public bool HandleStory(int? oldStoryId, StoryOnlyEditModel story)
        {
            return Story.HandleItem(Id, oldStoryId, story);
        }

        public void ResetId()
        {
            Id = null;
        }
    }
}
