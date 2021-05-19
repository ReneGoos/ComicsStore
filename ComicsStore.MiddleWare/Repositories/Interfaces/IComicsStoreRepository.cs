using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories.Interfaces
{
    public interface IComicsStoreRepository<T>
        where T : BasicsTable
    {
        Task<T> AddAsync(T value);
        Task DeleteAsync(T value);
        Task<List<T>> GetAsync();
        Task<T> UpdateAsync(T value);
    }
}