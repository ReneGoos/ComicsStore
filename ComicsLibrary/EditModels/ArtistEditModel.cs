using System.Collections.Generic;

namespace ComicsLibrary.EditModels
{
    public class ArtistEditModel : TableEditModel
    {
        private ICollection<StoryArtistEditModel> _storyArtists;

        public ICollection<StoryArtistEditModel> StoryArtist
        {
            get => _storyArtists;
            set
            {
                Set ( ref _storyArtists, value);
            }
        }
    }
}
