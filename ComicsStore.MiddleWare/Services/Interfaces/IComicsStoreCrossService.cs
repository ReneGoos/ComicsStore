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
        //Task<bool> AddMainAsync(int crossId, ICollection<TInMain> input);

        //Task DeleteMainAsync(int crossId);

        //Task<bool> ExistsMainAsync(int crossId);

        Task<ICollection<TOutMain>> GetMainAsync(int crossId);

        //Task<bool> UpdateMainAsync(int crossId, ICollection<TInMain> input);

        Task<bool> AddSubAsync(int mainId, ICollection<TInSub> input);

        //Task DeleteSubAsync(int mainId);

        //Task<bool> ExistsSubAsync(int mainId);

        Task<ICollection<TOutSub>> GetSubAsync(int mainId);

        //Task<bool> UpdateSubAsync(int mainId, ICollection<TInSub> input);
    }
}
