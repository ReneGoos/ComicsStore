using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using ComicsLibrary.Core;

namespace ComicsLibrary.ViewModels
{
    public class PublisherViewModel : BasicTableViewModel<IPublishersService, PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearch, PublisherEditModel>
    {
        public PublisherViewModel(IPublishersService publishersService,
            IMapper mapper) : base(publishersService, mapper)
        {
        }

        public void AddBookPublisher(ObservableChangedCollection<BookPublisherEditModel> bookPublishers, int? bookId)
        {
            Item.AddBookPublisher(bookPublishers, bookId);
        }

        public ObservableChangedCollection<BookPublisherEditModel> GetBookPublishers()
        {
            return Item.GetBookPublishers();
        }
    }
}