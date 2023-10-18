using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using ComicsStore.Data.Model;
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

        public void HandleArtist(int? oldArtistId, ArtistOnlyEditModel artist)
        {
            StoryArtist.HandleItem(Id, oldArtistId, artist);
        }

        public void HandleBook(int? oldBookId, BookOnlyEditModel book)
        {
            StoryBook.HandleItem(Id, oldBookId, book);
        }

        public void HandleCharacter(int? oldCharacterId, CharacterOnlyEditModel character)
        {
            StoryCharacter.HandleItem(Id, oldCharacterId, character);
        }

        public void HandleCode(int? oldCodeId, CodeOnlyEditModel code)
        {
            if (code == null)
            {
                CodeId = 0;
                return;
            }

            if (oldCodeId.HasValue && code.Id.Value == oldCodeId.Value && CodeId != oldCodeId.Value)
            {
                return;
            }

            if (CodeId != code.Id.Value)
            {
                CodeId = code.Id.Value;
            }
        }

        public void HandleOriginStory(int? oldOriginStoryId, StoryOnlyEditModel story)
        {
            if (story == null)
            {
                OriginStoryId = null;
                return;
            }

            if (oldOriginStoryId.HasValue && story.Id.Value == oldOriginStoryId.Value && CodeId != oldOriginStoryId.Value)
            {
                return;
            }

            if (OriginStoryId != story.Id.Value)
            {
                OriginStoryId = story.Id.Value;
            }
        }

        public void HandleStoryOrigin(int? oldOriginStoryId, StoryOnlyEditModel originStory)
        {
            StoryFromOrigin.HandleItem(Id, oldOriginStoryId, originStory);
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
