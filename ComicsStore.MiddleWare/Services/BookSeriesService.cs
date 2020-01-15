using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services.Interfaces;

namespace ComicsStore.MiddleWare.Services
{
    public class BookSeriesService : IBookSeriesService
    {
        private readonly IStoriesRepository _booksRepository;
        private readonly IComicsStoreRepository<Series, BasicSearchModel> _seriesRepository;
        private readonly IComicsStoreCrossRepository<BookSeries> _bookSeriesRepository;
        private readonly IMapper _mapper;

        public BookSeriesService(IStoriesRepository booksRepository,
            IComicsStoreRepository<Series, BasicSearchModel> seriesRepository,
            IComicsStoreCrossRepository<BookSeries> bookSeriesRepository,
            IMapper mapper)
        {
            _booksRepository = booksRepository;
            _seriesRepository = seriesRepository;
            _bookSeriesRepository = bookSeriesRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddSubAsync(int mainId, List<BookSeriesInputModel> input)
        {
            try
            {
                var bookSeries = _mapper.Map<List<BookSeries>>(input);

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
                return _mapper.Map<List<BookSeriesOutputModel>>(bookSeries);
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
                return _mapper.Map<List<SeriesBookOutputModel>>(bookSeries);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
