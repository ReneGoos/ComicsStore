using System.Collections.Generic;

namespace ComicsLibrary.EditModels
{
    public class SeriesEditModel : TableEditModel
    {
        private ICollection<BookSeriesEditModel> _bookSeries;
        private int? _seriesNumber;
        private string _seriesLanguage;

        public int? SeriesNumber { get => _seriesNumber; set { Set(ref _seriesNumber, value); } }
        public string SeriesLanguage { get => _seriesLanguage; set { Set(ref _seriesLanguage, value); } }
        public ICollection<BookSeriesEditModel> BookSeries { get => _bookSeries; set { Set(ref _bookSeries, value); } }
    }
}
