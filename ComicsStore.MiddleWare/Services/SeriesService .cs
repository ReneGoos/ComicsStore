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
    public class SeriesService : ISeriesService
    {
        private readonly IComicsStoreRepository<Series, BasicSearchModel> _seriesRepository;

        public SeriesService(IComicsStoreRepository<Series, BasicSearchModel> seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public async Task<SeriesOutputModel> AddAsync(SeriesInputModel seriesInput)
        {
            var series = Mapper.Map<Series>(seriesInput);

            var seriesResult = await _seriesRepository.AddAsync(series);

            return Mapper.Map<SeriesOutputModel>(seriesResult);
        }

        public async Task DeleteAsync(int id)
        {
            var series = await _seriesRepository.GetAsync(id);

            if (series == null)
            {
                return;
            }

            await _seriesRepository.DeleteAsync(series);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _seriesRepository.GetAsync(id) != null;
        }

        public async Task<List<SeriesOutputModel>> GetAsync(BasicSearchModel searchModel)
        {
            var series =  await _seriesRepository.GetAsync(searchModel);

            return Mapper.Map<List<SeriesOutputModel>>(series);
        }

        public async Task<SeriesOutputModel> GetAsync(int id)
        {
            var series = await _seriesRepository.GetAsync(id);

            return Mapper.Map<SeriesOutputModel>(series);
        }

        public async Task<SeriesOutputModel> UpdateAsync(int id, SeriesInputModel seriesInput)
        {
            var series = Mapper.Map<Series>(seriesInput);
            series.Id = id;

            series = await _seriesRepository.UpdateAsync(series);

            return Mapper.Map<SeriesOutputModel>(series);
        }

        public async Task<SeriesOutputModel> PatchAsync(int id, SeriesInputModel seriesInput)
        {
            var modifiedData = JsonHelper.ModifiedData<SeriesInputModel, Series>(seriesInput);

            var series = await _seriesRepository.PatchAsync(id, modifiedData);

            return Mapper.Map<SeriesOutputModel>(series);
        }
    }
}
