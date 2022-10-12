using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.EditModels
{
    public class ArtistEditModel : TableEditModel
    {
        private string _lastName;
        private string _firstName;
        private string _pseudonym;
        private string _realLastName;
        private string _realFirstName;
        private ObservableChangedCollection<ArtistStoryEditModel> _storyArtist;
        private ObservableChangedCollection<ArtistPseudonymEditModel> _mainArtist;
        private ObservableChangedCollection<PseudonymArtistEditModel> _pseudonymArtist;

        public ArtistEditModel() : base()
        {
            StoryArtist = new ObservableChangedCollection<ArtistStoryEditModel>();
            MainArtist = new ObservableChangedCollection<ArtistPseudonymEditModel>();
            PseudonymArtist = new ObservableChangedCollection<PseudonymArtistEditModel>();
        }

        [Required]
        public string LastName
        {
            get => _lastName;
            set
            {
                Set(ref _lastName, value);
                Name = value + ", " + FirstName;
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                Set(ref _firstName, value);
                Name = LastName + ", " + value;
            }
        }
        public string Pseudonym { get => _pseudonym; set => Set(ref _pseudonym, value); }
        public string RealLastName { get => _realLastName; set => Set(ref _realLastName, value); }
        public string RealFirstName { get => _realFirstName; set => Set(ref _realFirstName, value); }

        public ObservableChangedCollection<ArtistStoryEditModel> StoryArtist { get => _storyArtist; set => Set(ref _storyArtist, value); }
        public ObservableChangedCollection<ArtistPseudonymEditModel> MainArtist { get => _mainArtist; set => Set(ref _mainArtist, value); }
        public ObservableChangedCollection<PseudonymArtistEditModel> PseudonymArtist { get => _pseudonymArtist; set => Set(ref _pseudonymArtist, value); }

        public void AddStoryArtist(int? storyId, int? oldStoryId = null)
        {
            StoryArtist.HandleItem(Id, storyId, oldStoryId);
        }

        public void AddMainArtist(int? artistId, int? oldArtistId)
        {
            PseudonymArtist.HandleItem(Id, artistId, oldArtistId);
        }

        public void AddPseudonymArtist(int? artistId, int? oldArtistId)
        {
            MainArtist.HandleItem(Id, artistId, oldArtistId);
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
