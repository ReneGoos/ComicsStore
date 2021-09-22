using ComicsLibrary.Core;
using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class CodeEditModel : TableEditModel
    {
        private ObservableChangedCollection<SeriesCodeEditModel> _seriesCodes;
        private ObservableChangedCollection<StoryCodeEditModel> _storyCodes;

        public CodeEditModel() : base()
        {
            Series = new ObservableChangedCollection<SeriesCodeEditModel>();
            Story = new ObservableChangedCollection<StoryCodeEditModel>();
        }

        public ObservableChangedCollection<SeriesCodeEditModel> Series { get => _seriesCodes; set => Set(ref _seriesCodes, value); }
        public ObservableChangedCollection<StoryCodeEditModel> Story { get => _storyCodes; set => Set(ref _storyCodes, value); }

        public void AddSeriesCodes(int? seriesId)
        {
            if (seriesId.HasValue)
            {
                if (!Series.Any(s => s.SeriesId == seriesId.Value))
                {
                    Series.Add(new SeriesCodeEditModel
                    {
                        SeriesId = seriesId,
                        CodeId = Id
                    });
                }
            }
        }

        public void AddStoryCodes(int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!Story.Any(s => s.StoryId == storyId.Value))
                {
                    Story.Add(new StoryCodeEditModel
                    {
                        StoryId = storyId,
                        CodeId = Id
                    });
                }
            }
        }

        public List<SeriesCodeEditModel> GetSeriesCodes()
        {
            return new List<SeriesCodeEditModel>(Series.ToList().ConvertAll(s => new SeriesCodeEditModel
            {
                CodeId = s.CodeId,
                SeriesId = s.SeriesId
            }));
        }

        public List<StoryCodeEditModel> GetStoryCodes()
        {
            return new List<StoryCodeEditModel>(Story.ToList().ConvertAll(s => new StoryCodeEditModel
            {
                CodeId = s.CodeId,
                StoryId = s.StoryId
            }));
        }
    }
}
