using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services;
using ComicsStore.MiddleWare.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComicsStore.MiddleWare.Common
{
    public class ResolveDependencies
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("ComicsStore");
            services.AddDbContext<ComicsStoreDbContext>(options => options.UseSqlite(conn));

            services.AddScoped<IArtistsService, ArtistsService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<ICharactersService, CharactersService>();
            services.AddScoped<ICodesService, CodesService>();
            services.AddScoped<IPublishersService, PublishersService>();
            services.AddScoped<ISeriesService, SeriesService>();
            services.AddScoped<IStoriesService, StoriesService>();
            services.AddScoped<IExportBooksService, ExportBooksService>();

            services.AddScoped<IComicsStoreMainRepository<Artist, BasicSearchModel>, ArtistsRepository>();
            services.AddScoped<IComicsStoreMainRepository<Book, BasicSearchModel>, BooksRepository>();
            services.AddScoped<IComicsStoreMainRepository<Character, BasicSearchModel>, CharactersRepository>();
            services.AddScoped<IComicsStoreMainRepository<Code, BasicSearchModel>, CodesRepository>();
            services.AddScoped<IComicsStoreMainRepository<Publisher, BasicSearchModel>, PublishersRepository>();
            services.AddScoped<IComicsStoreMainRepository<Series, SeriesSearchModel>, SeriesRepository>();
            services.AddScoped<IComicsStoreMainRepository<Story, StorySearchModel>, StoriesRepository>();
            services.AddScoped<IExportBooksRepository, StorySeriesRepository>();

            services.AddScoped<IComicsStoreCrossRepository<BookPublisher, IBookPublisher>, BookPublishersRepository>();
            services.AddScoped<IComicsStoreCrossRepository<BookSeries, IBookSeries>, BookSeriesRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryArtist, IStoryArtist>, StoryArtistsRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryBook, IStoryBook>, StoryBooksRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter>, StoryCharactersRepository>();
        }
    }
}
