using AutoMapper;
using ComicsStore.Data.Model;
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

            services.AddScoped<IComicsStoreRepository<Artist, BasicSearchModel>, ArtistsRepository>();
            services.AddScoped<IComicsStoreRepository<Book, BasicSearchModel>, BooksRepository>();
            services.AddScoped<IComicsStoreRepository<Character, BasicSearchModel>, CharactersRepository>();
            services.AddScoped<IComicsStoreRepository<Code, BasicSearchModel>, CodesRepository>();
            services.AddScoped<IComicsStoreRepository<Publisher, BasicSearchModel>, PublishersRepository>();
            services.AddScoped<IComicsStoreRepository<Series, SeriesSearchModel>, SeriesRepository>();
            services.AddScoped<IStoriesRepository, StoriesRepository>();
            services.AddScoped<IExportBooksRepository, StorySeriesRepository>();

            services.AddScoped<IComicsStoreCrossRepository<BookPublisher>, BookPublishersRepository>();
            services.AddScoped<IComicsStoreCrossRepository<BookSeries>, BookSeriesRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryArtist>, StoryArtistsRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryBook>, StoryBooksRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryCharacter>, StoryCharactersRepository>();
        }
    }
}
