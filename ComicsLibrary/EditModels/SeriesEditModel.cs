using System;
using System.Collections.Generic;

namespace ComicsLibrary.EditModels
{
    public class SeriesEditModel : TableEditModel
    {
        private ICollection<BookSeriesEditModel> _bookSeries;
        private int? _seriesNumber;
        private string _seriesLanguage;
        private int _codeId;

        public SeriesEditModel() : base()
        {
            BookSeries = new List<BookSeriesEditModel>();
        }

        public int? SeriesNumber { get => _seriesNumber; set { Set(ref _seriesNumber, value); } }
        public string SeriesLanguage { get => _seriesLanguage; set { Set(ref _seriesLanguage, value); } }
        public int CodeId { get => _codeId; set { Set(ref _codeId, value); } }

        public void AddSeriesCode(int? codeId)
        {
            if (CodeId != codeId.Value)
            {
                CodeId = codeId.Value;
            }
        }

        public ICollection<BookSeriesEditModel> BookSeries { get => _bookSeries; set { Set(ref _bookSeries, value); } }
    }
}
