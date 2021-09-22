using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Collections.Generic;

namespace ComicsLibrary.ViewModels
{
    public class PublisherViewModel : BasicTableViewModel<IPublishersService, PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearch, PublisherEditModel>
    {
        public PublisherViewModel(IPublishersService publishersService,
            IMapper mapper) : base(publishersService, mapper)
        {
        }

        public void AddBookPublisher(int? bookId)
        {
            Item.AddBookPublisher(bookId);
        }

        public List<BookPublisherEditModel> GetBookPublishers()
        {
            return Item.GetBookPublishers();
        }
    }
}