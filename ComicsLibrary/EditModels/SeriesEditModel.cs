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

        public void HandleBook(int? oldBookId, BookOnlyEditModel book)
        {
            BookSeries.HandleItem(Id, oldBookId, book);
        }

        public void HandleCode(int? codeId)
        {
            if (CodeId != codeId.Value)
            {
                CodeId = codeId.Value;
            }
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
