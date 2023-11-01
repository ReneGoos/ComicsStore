using ComicsLibrary.Core;
using ComicsLibrary.Extensions;
using ComicsStore.MiddleWare.Models.Output;

namespace ComicsLibrary.EditModels
{
    public class SeriesEditModel : SeriesOnlyEditModel
    {
        private ObservableChangedCollection<SeriesBookEditModel> _bookSeries;

        public SeriesEditModel() : base()
        {
            BookSeries = new ObservableChangedCollection<SeriesBookEditModel>();
        }

        public CodeOnlyOutputModel Code { get; set; }
        public ObservableChangedCollection<SeriesBookEditModel> BookSeries { get => _bookSeries; set => Set(ref _bookSeries, value); }

        public bool HandleBook(int? oldBookId, BookOnlyEditModel book)
        {
            return BookSeries.HandleItem(Id, oldBookId, book);
        }

        public bool HandleCode(int? oldCodeId, CodeOnlyEditModel code)
        {
            if (code == null)
            {
                CodeId = 0;
                return oldCodeId.HasValue;
            }

            if (oldCodeId.HasValue && code.Id.Value == oldCodeId.Value && CodeId != oldCodeId.Value)
            {
                return false;
            }

            if (CodeId != code.Id.Value)
            {
                CodeId = code.Id.Value;
                return true;
            }
            return false;
        }

        public void ResetId()
        {
            Id = null;

            foreach (var book in BookSeries)
            {
                book.SeriesId = null;
            }
        }
    }
}
