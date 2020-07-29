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
    public class CodesService : ICodesService
    {
        private readonly IComicsStoreRepository<Code, BasicSearchModel> _codesRepository;
        private readonly IStoriesRepository _storiesRepository;
        private readonly IComicsStoreRepository<Series, SeriesSearchModel> _seriesRepository;
        private readonly IMapper _mapper;

        public CodesService(IComicsStoreRepository<Code, BasicSearchModel> codesRepository,
            IStoriesRepository storiesRepository,
            IComicsStoreRepository<Series, SeriesSearchModel> seriesRepository,
            IMapper mapper)
        {
            _codesRepository = codesRepository;
            _storiesRepository = storiesRepository;
            _seriesRepository = seriesRepository;
            _mapper = mapper;
        }

        public async Task<CodeOutputModel> AddAsync(CodeInputModel codeInput)
        {
            var code = _mapper.Map<Code>(codeInput);

            var codeResult = await _codesRepository.AddAsync(code);

            return _mapper.Map<CodeOutputModel>(codeResult);
        }

        public async Task DeleteAsync(int id)
        {
            var code = await _codesRepository.GetAsync(id);

            if (code == null)
            {
                return;
            }

            await _codesRepository.DeleteAsync(code);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _codesRepository.GetAsync(id) != null;
        }

        public async Task<List<CodeOutputModel>> GetAsync(BasicSearchModel searchModel)
        {
            var codes =  await _codesRepository.GetAsync(searchModel);

            return _mapper.Map<List<CodeOutputModel>>(codes);
        }

        public async Task<CodeOutputModel> GetAsync(int id)
        {
            var code = await _codesRepository.GetAsync(id);

            return _mapper.Map<CodeOutputModel>(code);
        }

        public async Task<CodeOutputModel> UpdateAsync(int id, CodeInputModel codeInput)
        {
            var code = _mapper.Map<Code>(codeInput);
            code.Id = id;

            code = await _codesRepository.UpdateAsync(code);

            return _mapper.Map<CodeOutputModel>(code);
        }

        public async Task<CodeOutputModel> PatchAsync(int id, CodeInputModel codeInput)
        {
            var modifiedData = JsonHelper.ModifiedData<CodeInputModel, Code>(codeInput, _mapper);

            var code = await _codesRepository.PatchAsync(id, modifiedData);

            return _mapper.Map<CodeOutputModel>(code);
        }

        public async Task<List<StoryOnlyOutputModel>> GetStoriesAsync(int codeId)
        {
            var storyCodes = await _storiesRepository.GetAsync(new StorySearchModel { CodeId = codeId });

            try
            {
                return _mapper.Map<List<StoryOnlyOutputModel>>(storyCodes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<SeriesOutputModel>> GetSeriesAsync(int codeId)
        {
            var seriesCodes = await _seriesRepository.GetAsync(new SeriesSearchModel { CodeId = codeId });

            try
            {
                return _mapper.Map<List<SeriesOutputModel>>(seriesCodes);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
