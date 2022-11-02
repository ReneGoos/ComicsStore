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

        [Required]
        public string BookType { get => _bookType; set => Set(ref _bookType, value); }
        [Required]
        public string Active { get => _active; set => Set(ref _active, value); }
        [Required]
        public int FirstYear { get => _firstYear; set => Set(ref _firstYear, value); }
        public int? ThisYear { get => _thisYear; set => Set(ref _thisYear, value); }
        [Required]
        public string FirstPrint { get => _firstPrint; set => Set(ref _firstPrint, value); }
    }
}
