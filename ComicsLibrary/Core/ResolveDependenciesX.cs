using ComicsStore.Data.Common;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Repositories;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services;
using ComicsStore.MiddleWare.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComicsLibrary.Core
{
    public class ResolveDependenciesX
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("ComicsStore");
            var optionsBuilder = new DbContextOptionsBuilder<ComicsStoreDbContext>();
            _ = services.AddDbContext<ComicsStoreDbContext>(options => options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlite(conn, p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .EnableSensitiveDataLogging());

            _ = services.AddScoped<IArtistsService, ArtistsService>();
            _ = services.AddScoped<IBooksService, BooksService>();
            _ = services.AddScoped<ICharactersService, CharactersService>();
            _ = services.AddScoped<ICodesService, CodesService>();
            _ = services.AddScoped<IPublishersService, PublishersService>();
            _ = services.AddScoped<ISeriesService, SeriesService>();
            _ = services.AddScoped<IStoriesService, StoriesService>();
            _ = services.AddScoped<IExportBooksService, ExportBooksService>();

            _ = services.AddScoped<IComicsStoreMainRepository<Artist, BasicSearch>, ArtistsRepository>();
            _ = services.AddScoped<IComicsStoreMainRepository<Book, BasicSearch>, BooksRepository>();
            _ = services.AddScoped<IComicsStoreMainRepository<Character, BasicSearch>, CharactersRepository>();
            _ = services.AddScoped<IComicsStoreMainRepository<Code, BasicSearch>, CodesRepository>();
            _ = services.AddScoped<IComicsStoreMainRepository<Publisher, BasicSearch>, PublishersRepository>();
            _ = services.AddScoped<IComicsStoreMainRepository<Series, SeriesSearch>, SeriesRepository>();
            _ = services.AddScoped<IComicsStoreMainRepository<Story, StorySearch>, StoriesRepository>();
            _ = services.AddScoped<IExportBooksRepository, StorySeriesRepository>();

            _ = services.AddScoped<IComicsStoreCrossRepository<BookPublisher, IBookPublisher>, BookPublishersRepository>();
            _ = services.AddScoped<IComicsStoreCrossRepository<BookSeries, IBookSeries>, BookSeriesRepository>();
            _ = services.AddScoped<IComicsStoreCrossRepository<StoryArtist, IStoryArtist>, StoryArtistsRepository>();
            _ = services.AddScoped<IComicsStoreCrossRepository<StoryBook, IStoryBook>, StoryBooksRepository>();
            _ = services.AddScoped<IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter>, StoryCharactersRepository>();
        }
    }
}
