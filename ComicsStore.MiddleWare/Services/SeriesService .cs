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

namespace ComicsStore.MiddleWare.Services
{
    public class SeriesService : ComicsStoreService<Series, SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearch>, ISeriesService
    {
        private readonly IComicsStoreCrossRepository<BookSeries, IBookSeries> _bookSeriesRepository;

        public SeriesService(IComicsStoreMainRepository<Series, SeriesSearch> seriesRepository,
            IComicsStoreCrossRepository<BookSeries, IBookSeries> bookSeriesRepository,
            IMapper mapper) : base(seriesRepository, mapper)
        {
            _bookSeriesRepository = bookSeriesRepository;
        }

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
}
