using ComicsLibrary.Core;
using ComicsStore.Data.Model;
using System;
using System.ComponentModel.DataAnnotations;
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
        private ObservableChangedCollection<StoryEditModel> _storyFromOrigin;
        private ObservableChangedCollection<StoryArtistEditModel> _storyArtist;
        private ObservableChangedCollection<StoryBookEditModel> _storyBook;
        private ObservableChangedCollection<StoryCharacterEditModel> _storyCharacter;

        public StoryEditModel() : base()
        {
            StoryFromOrigin = new ObservableChangedCollection<StoryEditModel>();
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

        public ObservableChangedCollection<StoryEditModel> StoryFromOrigin { get => _storyFromOrigin; set => Set(ref _storyFromOrigin, value); }
        public ObservableChangedCollection<StoryArtistEditModel> StoryArtist { get => _storyArtist; set => Set(ref _storyArtist, value); }
        public ObservableChangedCollection<StoryBookEditModel> StoryBook { get => _storyBook; set => Set(ref _storyBook, value); }
        public ObservableChangedCollection<StoryCharacterEditModel> StoryCharacter { get => _storyCharacter; set => Set(ref _storyCharacter, value); }

        public void AddStoryArtist(ObservableChangedCollection<StoryArtistEditModel> storyArtists, int? artistId)
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

        public void AddStoryBook(ObservableChangedCollection<StoryBookEditModel> storyBooks, int? bookId)
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

        public void AddStoryOrigin(int? originStoryId)
        {
            if (originStoryId.HasValue)
            {
                if (OriginStoryId != originStoryId.Value)
                {
                    OriginStoryId = originStoryId.Value;
                }
            }
        }

        public void AddStoryCharacter(ObservableChangedCollection<StoryCharacterEditModel> storyCharacters, int? characterId)
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

        public ObservableChangedCollection<StoryArtistEditModel> GetStoryArtists()
        {
            return new ObservableChangedCollection<StoryArtistEditModel>(StoryArtist.ToList().ConvertAll(s => new StoryArtistEditModel
            {
                ArtistId = s.ArtistId,
                StoryId = s.StoryId,
                ArtistType = s.ArtistType
            }));
        }

        public ObservableChangedCollection<StoryCharacterEditModel> GetStoryCharacters()
        {
            return new ObservableChangedCollection<StoryCharacterEditModel>(StoryCharacter.ToList().ConvertAll(s => new StoryCharacterEditModel
            {
                CharacterId = s.CharacterId,
                StoryId = s.StoryId
            }));
        }

        public ObservableChangedCollection<StoryBookEditModel> GetStoryBooks()
        {
            return new ObservableChangedCollection<StoryBookEditModel>(StoryBook.ToList().ConvertAll(s => new StoryBookEditModel
            {
                BookId = s.BookId,
                StoryId = s.StoryId
            }));
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
