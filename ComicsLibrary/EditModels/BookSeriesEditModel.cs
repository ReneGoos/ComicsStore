namespace ComicsLibrary.EditModels
{
    public class BookSeriesEditModel : CrossEditModel
    {
        private int _bookId;
        private int _seriesId;
        private string _issue;
        private decimal? _seriesOrder;

        public int BookId { get => _bookId; set { Set(ref _bookId, value); } }
        public int SeriesId { get => _seriesId; set { Set(ref _seriesId, value); } }
        public string Issue { get => _issue; set { Set(ref _issue, value); } }
        public decimal? SeriesOrder { get => _seriesOrder; set { Set(ref _seriesOrder, value); } }
    }
}