using ComicsLibrary.Core;
using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class ArtistEditModel : TableEditModel
    {
        public ArtistEditModel() : base()
        {
            StoryArtist = new ObservableChangedCollection<StoryArtistEditModel>();
        }

        public ObservableChangedCollection<StoryArtistEditModel> StoryArtist { get; set; }

        public void AddStoryArtist(int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!StoryArtist.Any(s => s.StoryId == storyId.Value))
                {
                    StoryArtist.Add(new StoryArtistEditModel
                    {
                        ArtistId = Id,
                        StoryId = storyId
                    });
                }
            }
        }

        public List<StoryArtistEditModel> GetStoryArtists()
        {
            return new List<StoryArtistEditModel>(StoryArtist.ToList().ConvertAll(s => new StoryArtistEditModel
            {
                ArtistId = s.ArtistId,
                StoryId = s.StoryId,
                ArtistType = s.ArtistType
            }));
        }
    }
}
