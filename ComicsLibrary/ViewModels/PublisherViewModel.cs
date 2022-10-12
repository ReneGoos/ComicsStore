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

namespace ComicsLibrary.ViewModels
{
    public class PublisherViewModel : BasicTableViewModel<IPublishersService, PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearch, PublisherEditModel>
    {
        public ICommand DeleteBookFromListCommand { get; protected set; }

        public PublisherViewModel(IPublishersService publishersService,
            INavigationService navigationService,
            IMapper mapper) : base(publishersService, navigationService, mapper)
        {
            DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
        }

        public void AddBookPublisher(int? bookId, int? oldBookId)
        {
            Item.AddBookPublisher(bookId, oldBookId);
        }

        public void DeleteBookFromList(int? bookId)
        {
            Item.AddBookPublisher(null, bookId);
        }
    }
}