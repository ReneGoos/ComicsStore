using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using System;
using ComicsStore.Data.Model.Interfaces;

namespace ComicsStore.MiddleWare.Repositories
{
    public class BooksRepository : ComicsStoreMainRepository<Book, BasicSearchModel>, IComicsStoreMainRepository<Book, BasicSearchModel>
    {
        private readonly IComicsStoreCrossRepository<BookPublisher, IBookPublisher> _bookPublishersRepository;
        private readonly IComicsStoreCrossRepository<BookSeries, IBookSeries> _bookSeriesRepository;
        private readonly IComicsStoreCrossRepository<StoryBook, IStoryBook> _storyBooksRepository;

        private bool UpdateLinkedItems(Book bookCurrent, Book bookNew)
        {
            _bookPublishersRepository.UpdateLinkedItems(bookCurrent, bookNew);
            _bookSeriesRepository.UpdateLinkedItems(bookCurrent, bookNew);
            _storyBooksRepository.UpdateLinkedItems(bookCurrent, bookNew);

            return true;
        }

        public BooksRepository(ComicsStoreDbContext context,
                               IComicsStoreCrossRepository<BookPublisher, IBookPublisher> bookPublishersRepository,
                               IComicsStoreCrossRepository<BookSeries, IBookSeries> bookSeriesRepository,
                               IComicsStoreCrossRepository<StoryBook, IStoryBook> storyBooksRepository
                               )
            : base(context)
        {
            _bookPublishersRepository = bookPublishersRepository;
            _bookSeriesRepository = bookSeriesRepository;
            _storyBooksRepository = storyBooksRepository;
        }

        public override Task<Book> AddAsync(Book book)
        {
            return AddItemAsync(_context.Books, book);
        }

        public override Task DeleteAsync(Book book)
        {
            return RemoveItemAsync(_context.Books, book);
        }

        public override Task<List<Book>> GetAsync()
        {
            var books = _context.Books
                .ToListAsync();

            return books;
        }

        public override Task<List<Book>> GetAsync(BasicSearchModel model)
        {
            var books = _context.Books
                .Include(b => b.StoryBook)
                .Include(b => b.BookSeries)
                .Include(b => b.BookPublisher)
                .Where(s => model.Name == null || s.Name.ToLower().Contains(model.Name.ToLower())).ToListAsync();

            return books;
        }

        public override Task<Book> GetAsync(int bookId, bool extended = false)
        {
            if (extended)
            {
                return _context.Books
                    .Include(b => b.StoryBook)
                    .ThenInclude(sb => sb.Story)
                    .Include(b => b.BookSeries)
                    .ThenInclude(bs => bs.Series)
                    .ThenInclude(s => s.Code)
                    .Include(b => b.BookPublisher)
                    .ThenInclude(bp => bp.Publisher)
                    .SingleOrDefaultAsync(b => b.Id == bookId);
            }

            return _context.Books
                .Include(b => b.StoryBook)
                .Include(b => b.BookSeries)
                .Include(b => b.BookPublisher)
                .SingleOrDefaultAsync(b => b.Id == bookId);
        }

        public override Task<Book> UpdateAsync(Book book)
        {
            return UpdateItemAsync(_context.Books, book, UpdateLinkedItems);
        }

        public override Task<Book> PatchAsync(int id, IDictionary<string, object> data = null)
        {
            return PatchItemAsync(_context.Books, id, data);
        }
    }
}