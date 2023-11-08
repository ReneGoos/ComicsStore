using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using System.ComponentModel;

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

        public bool HandleSeries(int? oldSeriesId, SeriesOnlyEditModel series, PropertyChangedEventHandler propertyChanged = null)
        {
            return Series.HandleItem(Id, oldSeriesId, series, propertyChanged);
        }

        public bool HandleStory(int? oldStoryId, StoryOnlyEditModel story, PropertyChangedEventHandler propertyChanged = null)
        {
            return Story.HandleItem(Id, oldStoryId, story, propertyChanged);
        }

        public void ResetId()
        {
            Id = null;
        }
    }
}
