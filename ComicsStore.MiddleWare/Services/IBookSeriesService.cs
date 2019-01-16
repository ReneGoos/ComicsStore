using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;

namespace ComicsStore.MiddleWare.Services
{
    public interface IBookSeriesService : IComicsStoreCrossService<SeriesBookOutputModel, BookSeriesInputModel, BookSeriesOutputModel>
    {
    }
}