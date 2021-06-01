using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;

namespace ComicsStore.Data.Repositories.Interfaces
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