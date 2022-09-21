using ComicsLibrary.Core;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ComicsLibrary.EditModels
{
    public class SeriesEditModel : TableEditModel
    {
        private ObservableChangedCollection<BookSeriesEditModel> _bookSeries;
        private int? _seriesNumber;
        private string _seriesLanguage;
        private int _codeId;

        public SeriesEditModel() : base()
        {
            BookSeries = new ObservableChangedCollection<BookSeriesEditModel>();
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

        public ObservableChangedCollection<BookSeriesEditModel> BookSeries { get => _bookSeries; set => Set(ref _bookSeries, value); }

        public void AddBookSeries(ObservableChangedCollection<BookSeriesEditModel> bookSeries, int? bookId)
        {
            if (bookId.HasValue)
            {
                if (!BookSeries.Any(s => s.BookId == bookId.Value))
                {
                    if (!bookSeries.Any(s => s.BookId == bookId.Value))
                    {
                        bookSeries.Add(new BookSeriesEditModel
                        {
                            SeriesId = Id,
                            BookId = bookId
                        });
                    }
                }

                BookSeries = bookSeries;
            }
        }

        public ObservableChangedCollection<BookSeriesEditModel> GetBookSeries()
        {
            return new ObservableChangedCollection<BookSeriesEditModel>(BookSeries.ToList().ConvertAll(s => new BookSeriesEditModel
            {
                BookId = s.BookId,
                SeriesId = s.SeriesId,
                Issue = s.Issue,
                SeriesOrder = s.SeriesOrder
            }));
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
