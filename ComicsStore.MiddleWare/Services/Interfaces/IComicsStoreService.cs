using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IComicsStoreService<TIn, TPatch, TOut, TSearch>
        where TIn : BasicInputModel
        where TPatch : BasicInputModel
        where TOut : BasicOutputModel
        where TSearch : BasicSearchModel
    {
        Task<TOut> AddAsync(TIn input);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<List<TOut>> GetAsync(TSearch searchModel);

        Task<TOut> GetAsync(int id);

        Task<TOut> UpdateAsync(int id, TIn input);

        Task<TOut> PatchAsync(int id, TPatch input);
    }
}
