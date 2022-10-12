using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.EditModels
{
    public class SeriesEditModel : TableEditModel
    {
        private ObservableChangedCollection<SeriesBookEditModel> _bookSeries;
        private int? _seriesNumber;
        private string _seriesLanguage;
        private int _codeId;

        public SeriesEditModel() : base()
        {
            BookSeries = new ObservableChangedCollection<SeriesBookEditModel>();
        }

        [Required]
        public int? SeriesNumber { get => _seriesNumber; set => Set(ref _seriesNumber, value); }
        [Required]
        public string SeriesLanguage { get => _seriesLanguage; set => Set(ref _seriesLanguage, value); }
        [Required]
        public int CodeId { get => _codeId; set => Set(ref _codeId, value); }

        public void AddSeriesCode(int? codeId)
        {
            if (CodeId != codeId.Value)
            {
                CodeId = codeId.Value;
            }
        }

        public ObservableChangedCollection<SeriesBookEditModel> BookSeries { get => _bookSeries; set => Set(ref _bookSeries, value); }

        public void AddBookSeries(int? bookId, int? oldBookId)
        {
            BookSeries.HandleItem(Id, bookId, oldBookId);
        }

        public override void ResetId()
        {
            Id = null;

            foreach (var book in BookSeries)
            {
                book.SeriesId = null;
            }
        }
    }
}
