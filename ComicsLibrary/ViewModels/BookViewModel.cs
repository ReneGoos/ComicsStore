using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using System;
using ComicsStore.Data.Model;

namespace ComicsLibrary.ViewModels
{
    public class BookViewModel : BasicTableViewModel<IBooksService, BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearch, BookEditModel>
    {
        public ICommand DeletePublisherFromListCommand { get; protected set; }
        public ICommand DeleteSeriesFromListCommand { get; protected set; }
        public ICommand DeleteStoryFromListCommand { get; protected set; }

        public BookViewModel(IBooksService booksService,
            INavigationService navigationService,
            IMapper mapper) : base(booksService, navigationService, mapper)
        {
            DeletePublisherFromListCommand = new RelayCommand<int?>(new Action<int?>(DeletePublisherFromList));
            DeleteSeriesFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteSeriesFromList));
            DeleteStoryFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteStoryFromList));
        }

        public void AddBookPublisher(int? publisherId, int? oldPublisherId)
        {
            Item.AddBookPublisher(publisherId, oldPublisherId);
        }

        private void DeletePublisherFromList(int? publisherId)
        {
            Item.AddBookPublisher(null, publisherId);
        }

        public void AddBookSeries(int? seriesId, int? oldSeriesId)
        {
            Item.AddBookSeries(seriesId, oldSeriesId);
        }

        private void DeleteSeriesFromList(int? seriesId)
        {
            Item.AddBookSeries(null, seriesId);
        }

        public void AddBookStory(int? storyId, int? oldStoryId)
        {
            Item.AddBookStory(storyId, oldStoryId);
        }

        private void DeleteStoryFromList(int? storyId)
        {
            Item.AddBookStory(null, storyId);
        }
    }
}