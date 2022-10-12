using ComicsStore.Data.Common;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Model.Search;
using ComicsStore.MiddleWare.Services;
using ComicsStore.MiddleWare.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ComicsStore.Data.Repositories;

namespace ComicsStore.MiddleWare.Common
{
    public class ResolveDependencies
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("ComicsStore");
            _ = services.AddDbContext<ComicsStoreDbContext>(options => options
                .UseSqlite(conn, p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .EnableSensitiveDataLogging());
            //                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

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
