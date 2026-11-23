using ComicsLibrary.EditModels;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.ListViewModels;

public class SeriesListViewModel : BasicListViewModel<ISeriesService,
                                                    SeriesInputModel,
                                                    SeriesInputModel,
                                                    SeriesOutputModel,
                                                    SeriesSearch>
{
    public SeriesListViewModel(ISeriesService itemService) : base(itemService)
    {
    }
}
