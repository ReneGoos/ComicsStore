using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using ComicsStore.Data.Model;
using System.ComponentModel;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class StoryEditModel : StoryOnlyEditModel
    {
        private ObservableChangedCollection<StoryOriginEditModel> _storyFromOrigin;
        private ObservableChangedCollection<StoryArtistEditModel> _storyArtist;
        private ObservableChangedCollection<StoryBookEditModel> _storyBook;
        private ObservableChangedCollection<StoryCharacterEditModel> _storyCharacter;

        public StoryEditModel() : base()
        {
            StoryFromOrigin = new ObservableChangedCollection<StoryOriginEditModel>();
            StoryArtist = new ObservableChangedCollection<StoryArtistEditModel>();
            StoryBook = new ObservableChangedCollection<StoryBookEditModel>();
            StoryCharacter = new ObservableChangedCollection<StoryCharacterEditModel>();
        }

        public CodeOnlyEditModel Code { get; set; }
        public StoryOnlyEditModel OriginStory { get; set; }

        public ObservableChangedCollection<StoryOriginEditModel> StoryFromOrigin { get => _storyFromOrigin; set => Set(ref _storyFromOrigin, value); }
        public ObservableChangedCollection<StoryArtistEditModel> StoryArtist { get => _storyArtist; set => Set(ref _storyArtist, value); }
        public ObservableChangedCollection<StoryBookEditModel> StoryBook { get => _storyBook; set => Set(ref _storyBook, value); }
        public ObservableChangedCollection<StoryCharacterEditModel> StoryCharacter { get => _storyCharacter; set => Set(ref _storyCharacter, value); }

        public bool HandleArtist(int? oldArtistId, ArtistOnlyEditModel artist, PropertyChangedEventHandler propertyChanged = null)
        {
            return StoryArtist.HandleItem(Id, oldArtistId, artist, propertyChanged);
        }

        public bool HandleBook(int? oldBookId, BookOnlyEditModel book, PropertyChangedEventHandler propertyChanged = null)
        {
            return StoryBook.HandleItem(Id, oldBookId, book, propertyChanged);
        }

        public bool HandleCharacter(int? oldCharacterId, CharacterOnlyEditModel character, PropertyChangedEventHandler propertyChanged = null)
        {
            return StoryCharacter.HandleItem(Id, oldCharacterId, character, propertyChanged);
        }

        public bool HandleCode(int? oldCodeId, CodeOnlyEditModel code)
        {
            if (code == null)
            {
                CodeId = 0;
                return oldCodeId.HasValue;
            }

            if (oldCodeId.HasValue && code.Id.Value == oldCodeId.Value && CodeId != oldCodeId.Value)
            {
                return false;
            }

            if (CodeId != code.Id.Value)
            {
                CodeId = code.Id.Value;
                return true;
            }
            return false;
        }

        public bool HandleOriginStory(int? oldOriginStoryId, StoryOnlyEditModel story)
        {
            if (story == null)
            {
                OriginStoryId = null;
                return oldOriginStoryId.HasValue;
            }

            if (oldOriginStoryId.HasValue && story.Id.Value == oldOriginStoryId.Value && CodeId != oldOriginStoryId.Value)
            {
                return false;
            }

            if (OriginStoryId != story.Id.Value)
            {
                OriginStoryId = story.Id.Value;
                return true;
            }
            return false;
        }

        public bool HandleStoryOrigin(int? oldOriginStoryId, StoryOnlyEditModel originStory, PropertyChangedEventHandler propertyChanged = null)
        {
            return StoryFromOrigin.HandleItem(Id, oldOriginStoryId, originStory, propertyChanged);
        }

        public void ResetId()
        {
            Id = null;

            foreach (var book in StoryBook)
            {
                book.StoryId = null;
            }


            foreach (var artist in StoryArtist)
            {
                artist.StoryId = null;
            }

            foreach (var character in StoryCharacter)
            {
                character.StoryId = null;
            }
        }
    }
}
