using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using System.Collections.Generic;
using System.ComponentModel;

namespace ComicsLibrary.EditModels
{
    public class ArtistEditModel : ArtistOnlyEditModel
    {
        private ObservableChangedCollection<ArtistStoryEditModel> _storyArtist;
        private ObservableChangedCollection<ArtistPseudonymEditModel> _mainArtist;
        private ObservableChangedCollection<PseudonymArtistEditModel> _pseudonymArtist;

        public ArtistEditModel() : base()
        {
            StoryArtist = [];
            MainArtist = [];
            PseudonymArtist = [];
        }

        public ObservableChangedCollection<ArtistStoryEditModel> StoryArtist { get => _storyArtist; set => Set(ref _storyArtist, value); }
        public ObservableChangedCollection<ArtistPseudonymEditModel> MainArtist { get => _mainArtist; set => Set(ref _mainArtist, value); }
        public ObservableChangedCollection<PseudonymArtistEditModel> PseudonymArtist { get => _pseudonymArtist; set => Set(ref _pseudonymArtist, value); }

        public bool HandleStory(int? oldStoryId, StoryOnlyEditModel story, PropertyChangedEventHandler propertyChanged = null)
        {
            return StoryArtist.HandleItem(Id, oldStoryId, story, propertyChanged);
        }

        public bool HandleMainArtist(int? oldArtistId, ArtistOnlyEditModel artist, PropertyChangedEventHandler propertyChanged = null)
        {
            return PseudonymArtist.HandleItem(Id, oldArtistId, artist, propertyChanged);
        }

        public bool HandlePseudonymArtist(int? oldArtistId, ArtistOnlyEditModel artist, PropertyChangedEventHandler propertyChanged = null)
        {
            return MainArtist.HandleItem(Id, oldArtistId, artist, propertyChanged);
        }

        public void ResetId()
        {
            Id = null;

            foreach (var story in StoryArtist)
            {
                story.ArtistId = null;
            }
        }

        public override bool Validate(Dictionary<string, List<string>> errors)
        {
            var validate = base.Validate(errors);

            if (Pseudonym is not null && Pseudonym.Equals("yes"))
            {
                if ((RealLastName is null || RealLastName.Length == 0) && (RealFirstName is null || RealFirstName.Length == 0) && PseudonymArtist.Count == 0)
                {
                    errors["RealName"] =
                    [
                        "Pseudonym requires a real name"
                    ];
                    validate = false;
                }
            }

            return validate;
        }
    }
}
