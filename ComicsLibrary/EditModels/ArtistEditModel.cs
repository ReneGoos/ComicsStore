using System.Collections.Generic;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class ArtistEditModel : TableEditModel
    {
        private ICollection<StoryArtistEditModel> _storyArtists;

        public ArtistEditModel() : base()
        {
            StoryArtist = new List<StoryArtistEditModel>();
        }

        public ICollection<StoryArtistEditModel> StoryArtist
        {
            get => _storyArtists;
            set
            {
                Set ( ref _storyArtists, value);
            }
        }

        public void AddStoryArtist(List<StoryArtistEditModel> storyArtists, int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!StoryArtist.Any(s => s.StoryId == storyId.Value))
                {
                    if (!storyArtists.Any(s => s.StoryId == storyId.Value))
                    {
                        storyArtists.Add(new StoryArtistEditModel
                        {
                            ArtistId = Id,
                            StoryId = storyId
                        });
                    }

                    StoryArtist = storyArtists;
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
