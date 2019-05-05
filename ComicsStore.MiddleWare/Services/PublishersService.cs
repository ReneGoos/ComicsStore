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

namespace ComicsStore.MiddleWare.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IComicsStoreRepository<Publisher, BasicSearchModel> _publishersRepository;

        public PublishersService(IComicsStoreRepository<Publisher, BasicSearchModel> publishersRepository)
        {
            _publishersRepository = publishersRepository;
        }

        public async Task<PublisherOutputModel> AddAsync(PublisherInputModel publisherInput)
        {
            var publisher = Mapper.Map<Publisher>(publisherInput);

            var publisherResult = await _publishersRepository.AddAsync(publisher);

            return Mapper.Map<PublisherOutputModel>(publisherResult);
        }

        public async Task DeleteAsync(int id)
        {
            var publisher = await _publishersRepository.GetAsync(id);

            if (publisher == null)
            {
                return;
            }

            await _publishersRepository.DeleteAsync(publisher);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _publishersRepository.GetAsync(id) != null;
        }

        public async Task<List<PublisherOutputModel>> GetAsync(BasicSearchModel searchModel)
        {
            var publishers =  await _publishersRepository.GetAsync(searchModel);

            return Mapper.Map<List<PublisherOutputModel>>(publishers);
        }

        public async Task<PublisherOutputModel> GetAsync(int id)
        {
            var publisher = await _publishersRepository.GetAsync(id);

            return Mapper.Map<PublisherOutputModel>(publisher);
        }

        public async Task<PublisherOutputModel> UpdateAsync(int id, PublisherInputModel publisherInput)
        {
            var publisher = Mapper.Map<Publisher>(publisherInput);
            publisher.Id = id;

            publisher = await _publishersRepository.UpdateAsync(publisher);

            return Mapper.Map<PublisherOutputModel>(publisher);
        }

        public async Task<PublisherOutputModel> PatchAsync(int id, PublisherInputModel publisherInput)
        {
            var modifiedData = JsonHelper.ModifiedData<PublisherInputModel, Publisher>(publisherInput);

            var publisher = await _publishersRepository.PatchAsync(id, modifiedData);

            return Mapper.Map<PublisherOutputModel>(publisher);
        }
    }
}
