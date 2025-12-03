using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.MiddleWare.Services;

public class SeriesService(IComicsStoreMainRepository<Series, SeriesSearch> seriesRepository,
    IComicsStoreCrossRepository<BookSeries, IBookSeries> bookSeriesRepository,
    IMapper mapper) : ComicsStoreService<Series, SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearch>(seriesRepository, mapper), ISeriesService
{
    private readonly IComicsStoreCrossRepository<BookSeries, IBookSeries> _bookSeriesRepository = bookSeriesRepository;

    public async Task<ICollection<SeriesBookOutputModel>> GetBooksAsync(int seriesId)
    {
        var bookSeries = await _bookSeriesRepository.GetAsync(null, seriesId);

        try
        {
            return Mapper.Map<ICollection<SeriesBookOutputModel>>(bookSeries);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
