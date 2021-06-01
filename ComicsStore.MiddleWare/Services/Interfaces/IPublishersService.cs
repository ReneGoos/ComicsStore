using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IPublishersService : IComicsStoreService<PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearch>
    {
        Task<ICollection<PublisherBookOutputModel>> GetBooksAsync(int publisherId);
    }
}