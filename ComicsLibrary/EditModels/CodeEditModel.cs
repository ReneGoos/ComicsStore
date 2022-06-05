using ComicsLibrary.Core;
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

        public void AddSeriesCodes(ObservableChangedCollection<SeriesCodeEditModel> seriesCodes, int? seriesId)
        {
            if (seriesId.HasValue)
            {
                if (!Series.Any(s => s.SeriesId == seriesId.Value))
                {
                    if (!seriesCodes.Any(s => s.SeriesId == seriesId.Value))
                    {
                        seriesCodes.Add(new SeriesCodeEditModel
                        {
                            SeriesId = seriesId,
                            CodeId = Id
                        });
                    }

                    Series = seriesCodes;
                }
            }
        }

        public void AddStoryCodes(ObservableChangedCollection<StoryCodeEditModel> storyCodes, int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!Story.Any(s => s.StoryId == storyId.Value))
                {
                    if (!storyCodes.Any(s => s.StoryId == storyId.Value))
                    {
                        storyCodes.Add(new StoryCodeEditModel
                        {
                            StoryId = storyId,
                            CodeId = Id
                        });
                    }

                    Story = storyCodes;
                }
            }
        }

        public ObservableChangedCollection<SeriesCodeEditModel> GetSeriesCodes()
        {
            return new ObservableChangedCollection<SeriesCodeEditModel>(Series.ToList().ConvertAll(s => new SeriesCodeEditModel
            {
                CodeId = s.CodeId,
                SeriesId = s.SeriesId
            }));
        }

        public ObservableChangedCollection<StoryCodeEditModel> GetStoryCodes()
        {
            return new ObservableChangedCollection<StoryCodeEditModel>(Story.ToList().ConvertAll(s => new StoryCodeEditModel
            {
                CodeId = s.CodeId,
                StoryId = s.StoryId
            }));
        }

        public override void ResetId()
        {
            Id = null;
        }
    }
}
