using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.MiddleWare.Common;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicsStore.MiddleWare.Services
{
    public class ComicsStoreService<T, TIn, TPatch, TOut, TSearch> : IComicsStoreService<TIn, TPatch, TOut, TSearch>
        where T : MainTable
        where TIn : BasicInputModel
        where TPatch : BasicInputModel
        where TOut : BasicOutputModel
        where TSearch : BasicSearch
    {

        private readonly IComicsStoreMainRepository<T, TSearch> _tableRepository;
        private readonly IMapper _mapper;

        public ComicsStoreService(IComicsStoreMainRepository<T, TSearch> tableRepository,
            IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        protected IMapper Mapper
        {
            get => _mapper;
        }

        public async Task<TOut> AddAsync(TIn itemInput)
        {
            var item = _mapper.Map<T>(itemInput);

            var itemResult = await _tableRepository.AddAsync(item);

            return _mapper.Map<TOut>(itemResult);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _tableRepository.GetAsync(id);

            if (item == null)
            {
                return;
            }

            await _tableRepository.DeleteAsync(item);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _tableRepository.GetAsync(id) != null;
        }

        public async Task<ICollection<TOut>> GetAsync()
        {
            var items = await _tableRepository.GetAsync();

            return _mapper.Map<ICollection<TOut>>(items);
        }

        public async Task<ICollection<TOut>> GetAsync(TSearch searchModel)
        {
            var items = await _tableRepository.GetAsync(searchModel);

            return _mapper.Map<ICollection<TOut>>(items);
        }

        public async Task<TOut> GetAsync(int id)
        {
            var item = await _tableRepository.GetAsync(id);

            return _mapper.Map<TOut>(item);
        }

        public async Task<TOut> UpdateAsync(int id, TIn itemInput)
        {
            var item = _mapper.Map<T>(itemInput);
            item.Id = id;

            item = await _tableRepository.UpdateAsync(item);

            return _mapper.Map<TOut>(item);
        }

        public async Task<TOut> PatchAsync(int id, TPatch itemInput)
        {
            var modifiedData = JsonHelper.ModifiedData<TPatch, T>(itemInput, _mapper);

            var item = await _tableRepository.PatchAsync(id, modifiedData);

            return _mapper.Map<TOut>(item);
        }
    }
}
