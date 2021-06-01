using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;

namespace ComicsStore.Data.Repositories.Interfaces
{
    public interface IComicsStoreMainRepository<T, TSearch> : IComicsStoreRepository<T>
        where T : BasicsTable
        where TSearch : BasicSearch
    {
        Task<T> GetAsync(int id, bool extended = false);
        Task<List<T>> GetAsync(TSearch model);
        Task<T> PatchAsync(int id, IDictionary<string, object> data);
    }
}