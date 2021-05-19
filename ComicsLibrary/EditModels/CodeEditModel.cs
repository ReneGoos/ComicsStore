using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class CodeEditModel : TableEditModel
    {
        private ICollection<SeriesCodeEditModel> _seriesCodes;
        private ICollection<StoryCodeEditModel> _storyCodes;

        public CodeEditModel() : base()
        {
            Series = new List<SeriesCodeEditModel>();
            Story = new List<StoryCodeEditModel>();
        }

        public ICollection<SeriesCodeEditModel> Series { get => _seriesCodes; set { Set(ref _seriesCodes, value); } }
        public ICollection<StoryCodeEditModel> Story { get => _storyCodes; set { Set(ref _storyCodes, value); } }

        public void AddSeriesCodes(List<SeriesCodeEditModel> seriesCodes, int? seriesId)
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

        public void AddStoryCodes(List<StoryCodeEditModel> storyCodes, int? storyId)
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
