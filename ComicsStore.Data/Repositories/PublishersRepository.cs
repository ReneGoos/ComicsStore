using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;

namespace ComicsStore.Data.Repositories
{
    public class PublishersRepository : ComicsStoreMainRepository<Publisher, BasicSearch>, IComicsStoreMainRepository<Publisher, BasicSearch>
    {
        private readonly IComicsStoreCrossRepository<BookPublisher, IBookPublisher> _bookPublishersRepository;

        private bool UpdateLinkedItems(Publisher publisherCurrent, Publisher publisherNew)
        {
            _bookPublishersRepository.UpdateLinkedItems(publisherCurrent, publisherNew);

            return true;
        }

        public PublishersRepository(ComicsStoreDbContext context,
                               IComicsStoreCrossRepository<BookPublisher, IBookPublisher> bookPublishersRepository)
            : base(context)
        {
            _bookPublishersRepository = bookPublishersRepository;
        }

        public override Task<Publisher> AddAsync(Publisher publisher)
        {
            return AddItemAsync(_context.Publishers, publisher);
        }

        public override Task DeleteAsync(Publisher publisher)
        {
            return RemoveItemAsync(_context.Publishers, publisher);
        }

        public override Task<List<Publisher>> GetAsync()
        {
            var publishers = _context.Publishers
                .ToListAsync();

            return publishers;
        }

        public override Task<List<Publisher>> GetAsync(BasicSearch model)
        {
            var publishers = _context.Publishers
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return publishers;
        }

        public override Task<Publisher> GetAsync(int publisherId, bool extended = false)
        {
            if (extended)
            {
                return _context.Publishers
                        .Include(p => p.BookPublisher)
                        .ThenInclude(pb => pb.Book)
                        .SingleOrDefaultAsync(p => p.Id == publisherId);
            }
            
            return _context.Publishers
                    .Include(p => p.BookPublisher)
                    .SingleOrDefaultAsync(p => p.Id == publisherId);
        }

        public override Task<Publisher> UpdateAsync(Publisher publisher)
        {
            return UpdateItemAsync(_context.Publishers, publisher, UpdateLinkedItems);
        }


        public override Task<Publisher> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Publishers, id, data);
        }
    }
}