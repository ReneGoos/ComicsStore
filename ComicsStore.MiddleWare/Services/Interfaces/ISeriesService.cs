using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface ISeriesService : IComicsStoreService<SeriesInputModel, SeriesInputModel, SeriesOutputModel, BasicSearchModel>
    {
    }
}