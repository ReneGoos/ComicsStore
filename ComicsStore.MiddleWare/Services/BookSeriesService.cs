using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class BookSeriesService : IBookSeriesService
    {
        private readonly IStoriesRepository _booksRepository;
        private readonly IComicsStoreRepository<Series, BasicSearchModel> _seriesRepository;
        private readonly IComicsStoreCrossRepository<BookSeries> _bookSeriesRepository;

        public BookSeriesService(IStoriesRepository booksRepository,
            IComicsStoreRepository<Series, BasicSearchModel> seriesRepository,
            IComicsStoreCrossRepository<BookSeries> bookSeriesRepository)
        {
            _booksRepository = booksRepository;
            _seriesRepository = seriesRepository;
            _bookSeriesRepository = bookSeriesRepository;
        }

        public async Task<bool> AddSubAsync(int mainId, List<BookSeriesInputModel> input)
        {
            try
            {
                var bookSeries = Mapper.Map<List<BookSeries>>(input);

                foreach (BookSeries item in bookSeries)
                {
                    item.BookId = mainId;
                    var bookSeriesResult = await _bookSeriesRepository.AddAsync(item);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<BookSeriesOutputModel>> GetSubAsync(int bookId)
        {
            var bookSeries = await _bookSeriesRepository.GetAsync(bookId, null);

            try
            {
                return Mapper.Map<List<BookSeriesOutputModel>>(bookSeries);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<SeriesBookOutputModel>> GetMainAsync(int seriesId)
        {
            var bookSeries = await _bookSeriesRepository.GetAsync(null, seriesId);

            try
            {
                return Mapper.Map<List<SeriesBookOutputModel>>(bookSeries);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
