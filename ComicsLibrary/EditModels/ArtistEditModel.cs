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

        public void AddStoryArtist(ObservableChangedCollection<StoryArtistEditModel> storyArtists, int? storyId)
        {
            if (storyId.HasValue)
            {
                if (!StoryArtist.Any(s => s.StoryId == storyId.Value))
                {
                    if (!storyArtists.Any(s => s.StoryId == storyId.Value))
                    {
                        StoryArtist.Add(new StoryArtistEditModel
                        {
                            ArtistId = Id,
                            StoryId = storyId
                        });
                    }

                    StoryArtist = storyArtists;
                }
            }
        }

        public ObservableChangedCollection<StoryArtistEditModel> GetStoryArtists()
        {
            return new ObservableChangedCollection<StoryArtistEditModel>(StoryArtist.ToList().ConvertAll(s => new StoryArtistEditModel
            {
                ArtistId = s.ArtistId,
                StoryId = s.StoryId,
                ArtistType = s.ArtistType
            }));
        }

        public override void ResetId()
        {
            Id = null;

            foreach (var story in StoryArtist)
            {
                story.ArtistId = null;
            }
        }
    }
}
