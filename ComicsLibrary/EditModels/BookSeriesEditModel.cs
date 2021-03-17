namespace ComicsLibrary.EditModels
{
    public class BookSeriesEditModel : CrossEditModel
    {
        private int _bookId;
        private int _seriesId;
        private string _issue;
        private decimal? _seriesOrder;

        public int BookId { get => _bookId; set { _bookId = value; RaisePropertyChanged(); } }
        public int SeriesId { get => _seriesId; set { _seriesId = value; RaisePropertyChanged(); } }
        public string Issue { get => _issue; set { _issue = value; RaisePropertyChanged(); } }
        public decimal? SeriesOrder { get => _seriesOrder; set { _seriesOrder = value; RaisePropertyChanged(); } }
    }
}