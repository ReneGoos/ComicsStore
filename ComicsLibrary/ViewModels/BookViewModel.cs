using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsLibrary.ViewModels
{
    public class BookViewModel : BasicTableViewModel<IBooksService, BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearchModel, BookEditModel>
    {
        public BookViewModel(IBooksService booksService,
            IMapper mapper) : base(booksService, mapper)
        {
        }
    }
}