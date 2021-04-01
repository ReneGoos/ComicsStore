using System.Collections.Generic;

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
        private ICollection<StoryArtistEditModel> _storyArtist;
        private ICollection<StoryBookEditModel> _storyBook;
        private ICollection<StoryCharacterEditModel> _storyCharacter;

        public string StoryType { get => _storyType; set { Set(ref _storyType, value); } }
        public int? StoryNumber { get => _storyNumber; set { Set(ref _storyNumber, value); } }
        public double? Pages { get => _pages; set { Set(ref _pages, value); } }
        public string ExtraInfo { get => _extraInfo; set { Set(ref _extraInfo, value); } }
        public string Language { get => _language; set { Set(ref _language, value); } }
        public int CodeId { get => _codeId; set { Set(ref _codeId, value); } }
        public int? OriginStoryId { get => _originStoryId; set { Set(ref _originStoryId, value); } }

        public ICollection<StoryArtistEditModel> StoryArtist { get => _storyArtist; set { Set(ref _storyArtist, value); } }
        public ICollection<StoryBookEditModel> StoryBook { get => _storyBook; set { Set(ref _storyBook, value); } }
        public ICollection<StoryCharacterEditModel> StoryCharacter { get => _storyCharacter; set { Set(ref _storyCharacter, value); } }
    }
}
