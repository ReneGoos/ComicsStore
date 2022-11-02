﻿using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Navigation;
using System.Windows.Input;
using ComicsLibrary.Core;
using System;
using ComicsStore.MiddleWare.Services;

namespace ComicsLibrary.ViewModels
{
    public class PublisherViewModel : BasicTableViewModel<IPublishersService, PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearch, PublisherEditModel>
    {
        private readonly IBooksService _booksService;

        public ICommand DeleteBookFromListCommand { get; protected set; }

        public PublisherViewModel(IPublishersService publishersService,
            IBooksService booksService,
            INavigationService navigationService,
            IMapper mapper) : base(publishersService, navigationService, mapper)
        {
            DeleteBookFromListCommand = new RelayCommand<int?>(new Action<int?>(DeleteBookFromList));
            _booksService = booksService;
        }

        public async void HandleBook(int? bookId, int? oldBookId)
        {
            var book = bookId.HasValue ? Mapper.Map<BookOnlyEditModel>(await _booksService.GetAsync(bookId.Value)) : null;
            Item.HandleBook(oldBookId, book);
        }

        public void DeleteBookFromList(int? bookId)
        {
            Item.HandleBook(bookId, null);
        }
    }
}