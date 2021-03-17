using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IBooksService : IComicsStoreService<BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearchModel>
    {
        Task<ICollection<BookPublisherOutputModel>> GetPublishersAsync(int bookId);
        Task<ICollection<BookSeriesOutputModel>> GetSeriesAsync(int bookId);
        Task<ICollection<BookStoryOutputModel>> GetStoriesAsync(int bookId);
    }
}
