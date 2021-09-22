using ComicsLibrary.Core;
using System.Collections.Generic;
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

        public void AddBookSeries(int? bookId)
        {
            if (bookId.HasValue)
            {
                if (!BookSeries.Any(s => s.BookId == bookId.Value))
                {
                    BookSeries.Add(new BookSeriesEditModel
                    {
                        SeriesId = Id,
                        BookId = bookId
                    });
                }
            }
        }

        public List<BookSeriesEditModel> GetBookSeries()
        {
            return new List<BookSeriesEditModel>(BookSeries.ToList().ConvertAll(s => new BookSeriesEditModel
            {
                BookId = s.BookId,
                SeriesId = s.SeriesId,
                Issue = s.Issue,
                SeriesOrder = s.SeriesOrder
            }));
        }
    }
}
