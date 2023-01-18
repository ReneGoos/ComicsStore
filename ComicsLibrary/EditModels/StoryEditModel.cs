using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
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

        public void HandleCode(int? codeId)
        {
            if (codeId.HasValue)
            {
                if (CodeId != codeId.Value)
                {
                    CodeId = codeId.Value;
                }
            }
        }

        public void HandleOriginStory(int? originStoryId, int? oldOriginStoryId)
        {
            if (originStoryId.HasValue)
            {
                if (OriginStoryId != originStoryId.Value)
                {
                    OriginStoryId = originStoryId.Value;
                }
            }
        }

        public void HandleStoryOrigin(int? oldOriginStoryId, StoryOnlyEditModel originStory)
        {
            if (originStory is null)
            {
                return;
            }

            if (oldOriginStoryId.HasValue)
            {
                var storyFromOrigin = StoryFromOrigin.FirstOrDefault(s => s.Id == oldOriginStoryId.Value);
                storyFromOrigin.Name = originStory.Name;
            }
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
