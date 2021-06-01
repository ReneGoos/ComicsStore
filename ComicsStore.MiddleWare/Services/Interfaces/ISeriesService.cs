using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface ISeriesService : IComicsStoreService<SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearch>
    {
        Task<ICollection<SeriesBookOutputModel>> GetBooksAsync(int seriesId);
    }
}