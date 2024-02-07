using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.EditModels
{
    public class StoryOnlyEditModel : TableEditModel
    {
        private static bool _listUpdating = false;

        private string _storyType;
        private decimal? _storyNumber;
        private double? _pages;
        private string _extraInfo;
        private string _language;
        private int _codeId;
        private int? _originStoryId;

        public static bool ListUpdating { private get => _listUpdating; set => _listUpdating = value; }
        [Required]
        public string StoryType { get => _storyType; set => Set(ref _storyType, value); }
        public decimal? StoryNumber { get => _storyNumber; set => Set(ref _storyNumber, value); }
        public double? Pages { get => _pages; set => Set(ref _pages, value); }
        public string ExtraInfo { get => _extraInfo; set => Set(ref _extraInfo, value); }
        [Required]
        public string Language { get => _language; set => Set(ref _language, value); }
        [Required]
        public int CodeId { get => _codeId; set => Set(ref _codeId, value); }
        public int? OriginStoryId { get => _originStoryId; set { if (!ListUpdating) { Set(ref _originStoryId, value); } } }
    }
}
