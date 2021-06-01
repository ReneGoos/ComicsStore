using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.Data.Model;

namespace ComicsStore.Data.Repositories.Interfaces
{
    public interface IComicsStoreCrossRepository<T, IObject>
        where T : CrossTable
    {
        Task<T> AddAsync(T value);
        Task DeleteAsync(T value);
        Task<T> UpdateAsync(T value);

        Task<List<T>> GetAsync(int? id, int? crossId);
        Task<List<T>> AddAsync(IEnumerable<T> value);
        Task DeleteAsync(IEnumerable<T> value);
        Task<List<T>> UpdateAsync(IEnumerable<T> value);
        void UpdateLinkedItems(IObject itemCurrent, IObject itemNew);
    }
}