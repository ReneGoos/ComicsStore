using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IPublishersService : IComicsStoreService<PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearchModel>
    {
        Task<List<BookOnlyOutputModel>> GetBooksAsync(int publisherId);
    }
}