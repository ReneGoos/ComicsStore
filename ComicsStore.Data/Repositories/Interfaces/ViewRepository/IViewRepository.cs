using ComicsStore.Data.Model.Output;
using ComicsStore.Data.Model.Search;

namespace ComicsStore.Data.Repositories.Interfaces;

public interface IViewRepository<T, TSearch> 
    where T : ResultView
    where TSearch : ISearch
{
    Task<List<T>> GetAsync(TSearch model);
}