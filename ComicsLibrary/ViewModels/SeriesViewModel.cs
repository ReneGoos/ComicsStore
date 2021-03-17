using AutoMapper;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsLibrary.ViewModels
{
    public class SeriesViewModel : BasicViewModel
    {
        private readonly ISeriesService _seriesService;

        public SeriesViewModel(ISeriesService seriesService,
            IMapper mapper) : base(mapper)
        {
            _seriesService = seriesService;
        }

    }
}