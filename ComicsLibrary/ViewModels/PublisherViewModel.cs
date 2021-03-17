using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsLibrary.ViewModels
{
    public class PublisherViewModel : BasicViewModel
    {
        private readonly IPublishersService _publishersService;

        public PublisherViewModel(IPublishersService publishersService,
            IMapper mapper) : base(mapper)
        {
            _publishersService = publishersService;
        }
    }
}