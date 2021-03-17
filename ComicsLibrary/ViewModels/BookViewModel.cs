using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsLibrary.ViewModels
{
    public class BookViewModel : BasicViewModel
    {
        private readonly IBooksService _booksService;

        public BookViewModel(IBooksService booksService,
            IMapper mapper) : base(mapper)
        {
            _booksService = booksService;
        }
    }
}