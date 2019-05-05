using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IBookSeriesService : IComicsStoreCrossService<SeriesBookOutputModel, BookSeriesInputModel, BookSeriesOutputModel>
    {
    }
}