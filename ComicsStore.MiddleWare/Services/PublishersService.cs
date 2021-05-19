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
    public class PublishersService : ComicsStoreService<Publisher, PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearchModel>, IPublishersService
    {
        private readonly IComicsStoreCrossRepository<BookPublisher, IBookPublisher> _bookPublishersRepository;

        public PublishersService(IComicsStoreMainRepository<Publisher, BasicSearchModel> publishersRepository,
            IComicsStoreCrossRepository<BookPublisher, IBookPublisher> bookPublishersRepository,
            IMapper mapper) : base(publishersRepository, mapper)
        {
            _bookPublishersRepository = bookPublishersRepository;
        }

        public async Task<ICollection<PublisherBookOutputModel>> GetBooksAsync(int publisherId)
        {
            var bookPublishers = await _bookPublishersRepository.GetAsync(null, publisherId);

            try
            {
                return Mapper.Map<ICollection<PublisherBookOutputModel>>(bookPublishers);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
