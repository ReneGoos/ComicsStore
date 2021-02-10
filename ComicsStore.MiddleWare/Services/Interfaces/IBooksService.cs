using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IBooksService : IComicsStoreService<BookInputModel, BookInputPatchModel, BookOutputModel, BasicSearchModel>
    {
        Task<List<BookPublisherOutputModel>> GetPublishersAsync(int bookId);
        Task<List<BookSeriesOutputModel>> GetSeriesAsync(int bookId);
        Task<List<StoryOnlyOutputModel>> GetStoriesAsync(int bookId);
    }
}
