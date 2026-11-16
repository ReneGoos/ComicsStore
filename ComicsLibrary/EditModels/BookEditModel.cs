using ComicsLibrary.Extensions;
using SoftGoosR.Common.Core;
using System.ComponentModel;

namespace ComicsLibrary.EditModels;

public class BookEditModel : BookOnlyEditModel
{
    private ObservableChangedCollection<BookArtistEditModel> _bookArtists;
    private ObservableChangedCollection<BookPublisherEditModel> _bookPublishers;
    private ObservableChangedCollection<BookSeriesEditModel> _bookSeries;
    private ObservableChangedCollection<BookStoryEditModel> _bookStories;

    public BookEditModel() : base()
    {
        BookArtist = [];
        BookPublisher = [];
        BookSeries = [];
        StoryBook = [];
    }

    public ObservableChangedCollection<BookArtistEditModel> BookArtist { get => _bookArtists; set => Set(ref _bookArtists, value); }
    public ObservableChangedCollection<BookPublisherEditModel> BookPublisher { get => _bookPublishers; set => Set(ref _bookPublishers, value); }
    public ObservableChangedCollection<BookSeriesEditModel> BookSeries { get => _bookSeries; set => Set(ref _bookSeries, value); }
    public ObservableChangedCollection<BookStoryEditModel> StoryBook { get => _bookStories; set => Set(ref _bookStories, value); }

    public bool HandleArtist(int? oldArtistId, ArtistOnlyEditModel artist, PropertyChangedEventHandler propertyChanged = null)
    {
        return BookArtist.HandleItem(Id, oldArtistId, artist, propertyChanged);
    }

    public bool HandlePublisher(int? oldPublisherId, PublisherOnlyEditModel publisher, PropertyChangedEventHandler propertyChanged = null)
    {
        return BookPublisher.HandleItem(Id, oldPublisherId, publisher, propertyChanged);
    }

    public bool HandleStory(int? oldStoryId, StoryOnlyEditModel story, PropertyChangedEventHandler propertyChanged = null)
    {
        return StoryBook.HandleItem(Id, oldStoryId, story, propertyChanged);
    }

    public bool HandleSeries(int? oldSeriesId, SeriesOnlyEditModel series, PropertyChangedEventHandler propertyChanged = null)
    {
        return BookSeries.HandleItem(Id, oldSeriesId, series, propertyChanged);
    }

    public void ResetId()
    {
        Id = null;

        foreach (var story in StoryBook)
        {
            story.BookId = null;
        }


        foreach (var publisher in BookPublisher)
        {
            publisher.BookId = null;
        }

        foreach (var series in BookSeries)
        {
            series.BookId = null;
        }
    }
}
