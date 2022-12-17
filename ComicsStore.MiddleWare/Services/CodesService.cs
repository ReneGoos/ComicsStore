using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Services.Interfaces;
using System;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.MiddleWare.Services
{
    public class CodesService : ComicsStoreService<Code, CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearch>, ICodesService
    {
        private readonly IComicsStoreMainRepository<Story, StorySearch> _storiesRepository;
        private readonly IComicsStoreMainRepository<Series, SeriesSearch> _seriesRepository;

        public CodesService(IComicsStoreMainRepository<Code, BasicSearch> codesRepository,
            IComicsStoreMainRepository<Story, StorySearch> storiesRepository,
            IComicsStoreMainRepository<Series, SeriesSearch> seriesRepository,
            IMapper mapper) : base(codesRepository, mapper)
        {
            _storiesRepository = storiesRepository;
            _seriesRepository = seriesRepository;
        }

        public async Task<ICollection<CodeStoryOutputModel>> GetStoriesAsync(int codeId)
        {
            var storyCodes = await _storiesRepository.GetAsync(new StorySearch { CodeId = codeId });

            try
            {
                return Mapper.Map<ICollection<CodeStoryOutputModel>>(storyCodes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ICollection<CodeSeriesOutputModel>> GetSeriesAsync(int codeId)
        {
            var seriesCodes = await _seriesRepository.GetAsync(new SeriesSearch { CodeId = codeId });

            try
            {
                return Mapper.Map<ICollection<CodeSeriesOutputModel>>(seriesCodes);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
