using System.Collections.Generic;
using System.Linq;

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
        private ICollection<StoryEditModel> _storyFromOrigin;
        private ICollection<StoryArtistEditModel> _storyArtist;
        private ICollection<StoryBookEditModel> _storyBook;
        private ICollection<StoryCharacterEditModel> _storyCharacter;

        public StoryEditModel() : base()
        {
            StoryFromOrigin = new List<StoryEditModel>();
            StoryArtist = new List<StoryArtistEditModel>();
            StoryBook = new List<StoryBookEditModel>();
            StoryCharacter = new List<StoryCharacterEditModel>();
        }

        public string StoryType { get => _storyType; set => Set(ref _storyType, value); }
        public int? StoryNumber { get => _storyNumber; set => Set(ref _storyNumber, value); }
        public double? Pages { get => _pages; set => Set(ref _pages, value); }
        public string ExtraInfo { get => _extraInfo; set => Set(ref _extraInfo, value); }
        public string Language { get => _language; set => Set(ref _language, value); }
        public int CodeId { get => _codeId; set => Set(ref _codeId, value); }
        public int? OriginStoryId { get => _originStoryId; set => Set(ref _originStoryId, value); }

        public ICollection<StoryEditModel> StoryFromOrigin { get => _storyFromOrigin; set => Set(ref _storyFromOrigin, value); }
        public ICollection<StoryArtistEditModel> StoryArtist { get => _storyArtist; set => Set(ref _storyArtist, value); }
        public ICollection<StoryBookEditModel> StoryBook { get => _storyBook; set => Set(ref _storyBook, value); }
        public ICollection<StoryCharacterEditModel> StoryCharacter { get => _storyCharacter; set => Set(ref _storyCharacter, value); }

        public void AddStoryArtist(ICollection<StoryArtistEditModel> storyArtists, int? artistId)
        {
            if (artistId.HasValue)
            {
                if (!StoryArtist.Any(s => s.ArtistId == artistId.Value))
                {
                    if (!storyArtists.Any(s => s.ArtistId == artistId.Value))
                    {
                        storyArtists.Add(new StoryArtistEditModel
                        {
                            ArtistId = artistId,
                            StoryId = Id
                        });
                    }

                    StoryArtist = storyArtists;
                }
            }
        }

        public void AddStoryBook(ICollection<StoryBookEditModel> storyBooks, int? bookId)
        {
            if (bookId.HasValue)
            {
                if (!StoryBook.Any(s => s.BookId == bookId.Value))
                {
                    if (!storyBooks.Any(s => s.BookId == bookId.Value))
                    {
                        storyBooks.Add(new StoryBookEditModel
                        {
                            BookId = bookId,
                            StoryId = Id
                        });
                    }

                    StoryBook = storyBooks;
                }
            }
        }

        public void AddStoryCode(int? codeId)
        {
            if (codeId.HasValue)
            {
                if (CodeId != codeId.Value)
                {
                    CodeId = codeId.Value;
                }
            }
        }

        public void AddStoryCharacter(ICollection<StoryCharacterEditModel> storyCharacters, int? characterId)
        {
            if (characterId.HasValue)
            {
                if (!StoryCharacter.Any(s => s.CharacterId == characterId.Value))
                {
                    if (!storyCharacters.Any(s => s.CharacterId == characterId.Value))
                    {
                        storyCharacters.Add(new StoryCharacterEditModel
                        {
                            CharacterId = characterId,
                            StoryId = Id
                        });
                    }

                    StoryCharacter = storyCharacters;
                }
            }
        }

        public List<StoryArtistEditModel> GetStoryArtists()
        {
            return new List<StoryArtistEditModel>(StoryArtist.ToList().ConvertAll(s => new StoryArtistEditModel
            {
                ArtistId = s.ArtistId,
                StoryId = s.StoryId,
                ArtistType = s.ArtistType
            }));
        }

        public List<StoryCharacterEditModel> GetStoryCharacters()
        {
            return new List<StoryCharacterEditModel>(StoryCharacter.ToList().ConvertAll(s => new StoryCharacterEditModel
            {
                CharacterId = s.CharacterId,
                StoryId = s.StoryId
            }));
        }

        public List<StoryBookEditModel> GetStoryBooks()
        {
            return new List<StoryBookEditModel>(StoryBook.ToList().ConvertAll(s => new StoryBookEditModel
            {
                BookId = s.BookId,
                StoryId = s.StoryId
            }));
        }
    }
}
