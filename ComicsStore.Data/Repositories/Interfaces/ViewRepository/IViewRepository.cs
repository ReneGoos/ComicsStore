using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Output;
using ComicsStore.Data.Model.Search;

namespace ComicsStore.Data.Repositories.Interfaces
{
    public interface IViewRepository<T, TSearch> 
        where T : ResultView
        where TSearch : IViewSearch
    {
        Task<List<T>> GetAsync(TSearch model);
    }
}