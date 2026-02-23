using ComicsLibrary.Extensions;
using SoftGoosR.Common.Core;
using System.ComponentModel;

namespace ComicsLibrary.EditModels;

public class PublisherEditModel : PublisherOnlyEditModel
{
    private ObservableChangedCollection<PublisherBookEditModel> _bookPublishers;

    public PublisherEditModel() : base()
    {
        BookPublisher = [];
    }

    public ObservableChangedCollection<PublisherBookEditModel> BookPublisher { get => _bookPublishers; set => Set(ref _bookPublishers, value); }

    public bool HandleBook(int? oldBookId, BookOnlyEditModel book, PropertyChangedEventHandler propertyChanged = null)
    {

        return BookPublisher.HandleItem(Id, oldBookId, book, propertyChanged);
    }

    public void ResetId()
    {
        Id = null;

        foreach (var book in BookPublisher)
        {
            book.PublisherId = null;
        }
    }
}
