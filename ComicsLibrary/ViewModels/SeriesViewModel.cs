using AutoMapper;
using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;

namespace ComicsLibrary.ViewModels
{
    public class SeriesViewModel : BasicTableViewModel<ISeriesService, SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearchModel, SeriesEditModel>
    {
        public SeriesViewModel(ISeriesService seriesService,
            IMapper mapper) : base(seriesService, mapper)
        {
        }

        internal void AddSeriesCode(int? codeId)
        {
            Item.AddSeriesCode(codeId);
        }
    }
}