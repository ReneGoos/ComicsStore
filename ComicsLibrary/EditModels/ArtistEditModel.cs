using ComicsLibrary.Core;
using ComicsLibrary.Extensions;

namespace ComicsLibrary.EditModels
{
    public class ArtistEditModel : ArtistOnlyEditModel
    {
        private ObservableChangedCollection<ArtistStoryEditModel> _storyArtist;
        private ObservableChangedCollection<ArtistPseudonymEditModel> _mainArtist;
        private ObservableChangedCollection<PseudonymArtistEditModel> _pseudonymArtist;

        public ArtistEditModel() : base()
        {
            StoryArtist = new ObservableChangedCollection<ArtistStoryEditModel>();
            MainArtist = new ObservableChangedCollection<ArtistPseudonymEditModel>();
            PseudonymArtist = new ObservableChangedCollection<PseudonymArtistEditModel>();
        }

        public ObservableChangedCollection<ArtistStoryEditModel> StoryArtist { get => _storyArtist; set => Set(ref _storyArtist, value); }
        public ObservableChangedCollection<ArtistPseudonymEditModel> MainArtist { get => _mainArtist; set => Set(ref _mainArtist, value); }
        public ObservableChangedCollection<PseudonymArtistEditModel> PseudonymArtist { get => _pseudonymArtist; set => Set(ref _pseudonymArtist, value); }

        public void HandleStory(int? oldStoryId, StoryOnlyEditModel story)
        {
            StoryArtist.HandleItem(Id, oldStoryId, story);
        }

        public void HandleMainArtist(int? oldArtistId, ArtistOnlyEditModel artist)
        {
            PseudonymArtist.HandleItem(Id, oldArtistId, artist);
        }

        public void HandlePseudonymArtist(int? oldArtistId, ArtistOnlyEditModel artist)
        {
            MainArtist.HandleItem(Id, oldArtistId, artist);
        }

        public void ResetId()
        {
            Id = null;

            foreach (var story in StoryArtist)
            {
                story.ArtistId = null;
            }
        }
    }
}
