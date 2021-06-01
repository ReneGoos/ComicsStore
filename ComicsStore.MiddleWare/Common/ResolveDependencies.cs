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
            services.AddDbContext<ComicsStoreDbContext>(options => options.UseSqlite(conn, p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddScoped<IArtistsService, ArtistsService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<ICharactersService, CharactersService>();
            services.AddScoped<ICodesService, CodesService>();
            services.AddScoped<IPublishersService, PublishersService>();
            services.AddScoped<ISeriesService, SeriesService>();
            services.AddScoped<IStoriesService, StoriesService>();
            services.AddScoped<IExportBooksService, ExportBooksService>();

            services.AddScoped<IComicsStoreMainRepository<Artist, BasicSearch>, ArtistsRepository>();
            services.AddScoped<IComicsStoreMainRepository<Book, BasicSearch>, BooksRepository>();
            services.AddScoped<IComicsStoreMainRepository<Character, BasicSearch>, CharactersRepository>();
            services.AddScoped<IComicsStoreMainRepository<Code, BasicSearch>, CodesRepository>();
            services.AddScoped<IComicsStoreMainRepository<Publisher, BasicSearch>, PublishersRepository>();
            services.AddScoped<IComicsStoreMainRepository<Series, SeriesSearch>, SeriesRepository>();
            services.AddScoped<IComicsStoreMainRepository<Story, StorySearch>, StoriesRepository>();
            services.AddScoped<IExportBooksRepository, StorySeriesRepository>();

            services.AddScoped<IComicsStoreCrossRepository<BookPublisher, IBookPublisher>, BookPublishersRepository>();
            services.AddScoped<IComicsStoreCrossRepository<BookSeries, IBookSeries>, BookSeriesRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryArtist, IStoryArtist>, StoryArtistsRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryBook, IStoryBook>, StoryBooksRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter>, StoryCharactersRepository>();
        }
    }
}
