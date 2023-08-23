using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;

namespace ComicsStore.Data.Repositories.Interfaces.MainRepository
{
    public interface IComicsStoreMainRepository<T, TSearch> : IComicsStoreRepository<T>
        where T : BasicsTable
        where TSearch : IBasicSearch
    {
        Task<T> GetAsync(int id, bool extended);
        Task<List<T>> GetAsync(TSearch model);
        Task<T> PatchAsync(int id, IDictionary<string, object> data);
    }
}