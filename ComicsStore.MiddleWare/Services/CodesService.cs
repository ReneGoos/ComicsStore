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

namespace ComicsStore.MiddleWare.Services
{
    public class CodesService : ComicsStoreService<Code, CodeInputModel, CodeInputModel, CodeOutputModel, BasicSearchModel>, ICodesService
    {
        private readonly IComicsStoreMainRepository<Story, StorySearchModel> _storiesRepository;
        private readonly IComicsStoreMainRepository<Series, SeriesSearchModel> _seriesRepository;

        public CodesService(IComicsStoreMainRepository<Code, BasicSearchModel> codesRepository,
            IComicsStoreMainRepository<Story, StorySearchModel> storiesRepository,
            IComicsStoreMainRepository<Series, SeriesSearchModel> seriesRepository,
            IMapper mapper) : base(codesRepository, mapper)
        {
            _storiesRepository = storiesRepository;
            _seriesRepository = seriesRepository;
        }

        public async Task<ICollection<CodeStoryOutputModel>> GetStoriesAsync(int codeId)
        {
            var storyCodes = await _storiesRepository.GetAsync(new StorySearchModel { CodeId = codeId });

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
            var seriesCodes = await _seriesRepository.GetAsync(new SeriesSearchModel { CodeId = codeId });

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
