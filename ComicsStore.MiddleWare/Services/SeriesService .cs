using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.MiddleWare.Services
{
    public class SeriesService : ComicsStoreService<Series, SeriesInputModel, SeriesInputModel, SeriesOutputModel, SeriesSearchModel>, ISeriesService
    {
        private readonly IComicsStoreCrossRepository<BookSeries, IBookSeries> _bookSeriesRepository;

        public SeriesService(IComicsStoreMainRepository<Series, SeriesSearchModel> seriesRepository,
            IComicsStoreCrossRepository<BookSeries, IBookSeries> bookSeriesRepository,
            IMapper mapper) : base (seriesRepository, mapper)
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
