using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.EditModels
{
    public class BookOnlyEditModel : TableEditModel
    {
        private string _bookType;
        private string _active;
        private int _firstYear;
        private int? _thisYear;
        private string _firstPrint;
        private string _signed;
        private string _checked;
        private string _hardCover;

        [Required]
        public string BookType { get => _bookType; set => Set(ref _bookType, value); }
        [Required]
        public string Active { get => _active; set => Set(ref _active, value); }
        [Required]
        public int FirstYear { get => _firstYear; set => Set(ref _firstYear, value); }
        public int? ThisYear { get => _thisYear; set => Set(ref _thisYear, value); }
        [Required]
        public string FirstPrint { get => _firstPrint; set => Set(ref _firstPrint, value); }
        public string Signed { get => _signed; set => Set(ref _signed, value); }
        public string Checked { get => _checked; set => Set(ref _checked, value); }
        public string CoverType { get => _hardCover; set => Set(ref _hardCover, value); }
    }
}
