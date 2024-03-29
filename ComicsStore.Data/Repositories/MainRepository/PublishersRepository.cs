﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Common;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;

namespace ComicsStore.Data.Repositories.MainRepository
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

        public override Task<Publisher> AddAsync(Publisher value)
        {
            return AddItemAsync(_context.Publishers, value);
        }

        public override Task DeleteAsync(Publisher value)
        {
            return RemoveItemAsync(_context.Publishers, value);
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

        public override Task<Publisher> GetAsync(int publisherId, bool extended)
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

        public override Task<Publisher> UpdateAsync(Publisher value)
        {
            return UpdateItemAsync(_context.Publishers, value, UpdateLinkedItems);
        }


        public override Task<Publisher> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Publishers, id, data);
        }
    }
}