using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.EditModels
{
    public class StoryEditModel : TableEditModel
    {
        private string _storyType;
        private int? _storyNumber;
        private double? _pages;
        private string _extraInfo;
        private string _language;
        private int _codeId;
        private int? _originStoryId;
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

        [Required]
        public string StoryType { get => _storyType; set => Set(ref _storyType, value); }
        public int? StoryNumber { get => _storyNumber; set => Set(ref _storyNumber, value); }
        public double? Pages { get => _pages; set => Set(ref _pages, value); }
        public string ExtraInfo { get => _extraInfo; set => Set(ref _extraInfo, value); }
        [Required]
        public string Language { get => _language; set => Set(ref _language, value); }
        [Required]
        public int CodeId { get => _codeId; set => Set(ref _codeId, value); }
        public int? OriginStoryId { get => _originStoryId; set => Set(ref _originStoryId, value); }

        public ObservableChangedCollection<StoryOriginEditModel> StoryFromOrigin { get => _storyFromOrigin; set => Set(ref _storyFromOrigin, value); }
        public ObservableChangedCollection<StoryArtistEditModel> StoryArtist { get => _storyArtist; set => Set(ref _storyArtist, value); }
        public ObservableChangedCollection<StoryBookEditModel> StoryBook { get => _storyBook; set => Set(ref _storyBook, value); }
        public ObservableChangedCollection<StoryCharacterEditModel> StoryCharacter { get => _storyCharacter; set => Set(ref _storyCharacter, value); }

        public void AddStoryArtist(int? artistId, int? oldArtistId)
        {
            StoryArtist.HandleItem(Id, artistId, oldArtistId);
        }

        public void AddStoryBook(int? bookId, int? oldBookId)
        {
            StoryBook.HandleItem(Id, bookId, oldBookId);
        }

        public void AddStoryCharacter(int? characterId, int? oldCharacterId)
        {
            StoryCharacter.HandleItem(Id, characterId, oldCharacterId);
        }

        public void AddStoryCode(int? codeId, int? oldCodeId)
        {
            if (codeId.HasValue)
            {
                if (CodeId != codeId.Value)
                {
                    CodeId = codeId.Value;
                }
            }
        }

        public void AddOriginStory(int? originStoryId, int? oldOriginStoryId)
        {
            if (originStoryId.HasValue)
            {
                if (OriginStoryId != originStoryId.Value)
                {
                    OriginStoryId = originStoryId.Value;
                }
            }
        }

        public void AddStoryOrigin(int? originStoryId, int? oldOriginStoryId)
        {
            StoryFromOrigin.HandleItem(Id, originStoryId, oldOriginStoryId);
        }

        public override void ResetId()
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
