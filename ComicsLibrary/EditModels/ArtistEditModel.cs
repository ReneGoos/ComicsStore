using ComicsLibrary.Core;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class ArtistEditModel : TableEditModel
    {
        private string _lastName;
        private string _firstName;
        private string _pseudonym;
        private string _realLastName;
        private string _realFirstName;
        private ObservableChangedCollection<StoryArtistEditModel> _storyArtist;
        private ObservableChangedCollection<PseudonymEditModel> _mainArtist;
        private ObservableChangedCollection<PseudonymEditModel> _pseudonymArtist;

        public ArtistEditModel() : base()
        {
            StoryArtist = new ObservableChangedCollection<StoryArtistEditModel>();
            MainArtist = new ObservableChangedCollection<PseudonymEditModel>();
            PseudonymArtist = new ObservableChangedCollection<PseudonymEditModel>();
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

        public ObservableChangedCollection<StoryArtistEditModel> StoryArtist { get => _storyArtist; set => Set(ref _storyArtist, value); }
        public ObservableChangedCollection<PseudonymEditModel> MainArtist { get => _mainArtist; set => Set(ref _mainArtist, value); }
        public ObservableChangedCollection<PseudonymEditModel> PseudonymArtist { get => _pseudonymArtist; set => Set(ref _pseudonymArtist, value); }

        public void AddStoryArtist(ObservableChangedCollection<StoryArtistEditModel> storyArtists, int? storyId)
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
                }

                StoryArtist = storyArtists;
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

        public void AddMainArtist(ObservableChangedCollection<PseudonymEditModel> mainArtists, int? artistId)
        {
            if (artistId.HasValue)
            {
                if (!PseudonymArtist.Any(a => a.MainArtistId == artistId.Value))
                {
                    if (!mainArtists.Any(a => a.MainArtistId == artistId.Value))
                    {
                        mainArtists.Add(new PseudonymEditModel
                        {
                            PseudonymArtistId = Id,
                            MainArtistId = artistId
                        });
                    }
                }

                PseudonymArtist = mainArtists;
            }
        }

        public void AddPseudonymArtist(ObservableChangedCollection<PseudonymEditModel> pseudonymArtists, int? artistId)
        {
            if (artistId.HasValue)
            {
                if (!MainArtist.Any(a => a.PseudonymArtistId == artistId.Value))
                {
                    if (!pseudonymArtists.Any(a => a.PseudonymArtistId == artistId.Value))
                    {
                        pseudonymArtists.Add(new PseudonymEditModel
                        {
                            MainArtistId = Id,
                            PseudonymArtistId = artistId
                        });
                    }
                }

                MainArtist = pseudonymArtists;
            }
        }

        public ObservableChangedCollection<PseudonymEditModel> GetMainArtists()
        {
            return new ObservableChangedCollection<PseudonymEditModel>(PseudonymArtist.ToList().ConvertAll(a => new PseudonymEditModel
            {
                MainArtistId = a.MainArtistId,
                PseudonymArtistId = a.PseudonymArtistId
            }));
        }

        public ObservableChangedCollection<PseudonymEditModel> GetPseudonymArtists()
        {
            return new ObservableChangedCollection<PseudonymEditModel>(MainArtist.ToList().ConvertAll(a => new PseudonymEditModel
            {
                MainArtistId = a.MainArtistId,
                PseudonymArtistId = a.PseudonymArtistId
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
