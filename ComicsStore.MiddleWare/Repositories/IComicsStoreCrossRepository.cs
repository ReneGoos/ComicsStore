using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Repositories
{
    public interface IComicsStoreCrossRepository<T>
        where T : CrossTable
    {
        Task<T> AddAsync(T value);
        Task DeleteAsync(T value);
        Task<T> UpdateAsync(T value);
    }
}