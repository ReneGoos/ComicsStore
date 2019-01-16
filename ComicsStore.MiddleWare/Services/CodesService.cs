using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class CodesService : ICodesService
    {
        private readonly IComicsStoreRepository<Code, BasicSearchModel> _codesRepository;

        public CodesService(IComicsStoreRepository<Code, BasicSearchModel> codesRepository)
        {
            _codesRepository = codesRepository;
        }

        public async Task<CodeOutputModel> AddAsync(CodeInputModel codeInput)
        {
            var code = Mapper.Map<Code>(codeInput);

            var codeResult = await _codesRepository.AddAsync(code);

            return Mapper.Map<CodeOutputModel>(codeResult);
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

            return Mapper.Map<List<CodeOutputModel>>(codes);
        }

        public async Task<CodeOutputModel> GetAsync(int id)
        {
            var code = await _codesRepository.GetAsync(id);

            return Mapper.Map<CodeOutputModel>(code);
        }

        public async Task<CodeOutputModel> UpdateAsync(int id, CodeInputModel codeInput)
        {
            var code = Mapper.Map<Code>(codeInput);
            code.Id = id;

            code = await _codesRepository.UpdateAsync(code);

            return Mapper.Map<CodeOutputModel>(code);
        }

        public async Task<CodeOutputModel> PatchAsync(int id, CodeInputModel codeInput)
        {
            var modifiedData = JsonHelper.ModifiedData<CodeInputModel, Code>(codeInput);

            var code = await _codesRepository.PatchAsync(id, modifiedData);

            return Mapper.Map<CodeOutputModel>(code);
        }
    }
}
