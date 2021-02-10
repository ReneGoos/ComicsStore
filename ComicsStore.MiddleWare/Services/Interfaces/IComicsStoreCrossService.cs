using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IComicsStoreCrossService<TOutMain, TInSub, TOutSub>
    //where TInMain : BasicInputModel
    where TInSub : BasicCrossInputModel
    where TOutMain : BasicOutputModel
    where TOutSub : BasicOutputModel
    {
        //Task<bool> AddMainAsync(int crossId, List<TInMain> input);

        //Task DeleteMainAsync(int crossId);

        //Task<bool> ExistsMainAsync(int crossId);

        Task<List<TOutMain>> GetMainAsync(int crossId);

        //Task<bool> UpdateMainAsync(int crossId, List<TInMain> input);

        Task<bool> AddSubAsync(int mainId, List<TInSub> input);

        //Task DeleteSubAsync(int mainId);

        //Task<bool> ExistsSubAsync(int mainId);

        Task<List<TOutSub>> GetSubAsync(int mainId);

        //Task<bool> UpdateSubAsync(int mainId, List<TInSub> input);
    }
}
