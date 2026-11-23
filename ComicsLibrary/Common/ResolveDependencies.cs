using ComicsLibrary.ListViewModels;
using ComicsLibrary.ViewModels;
using ComicsStore.Data.Common;
using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Interfaces;
using ComicsStore.Data.Model.Output;
using ComicsStore.Data.Model.Search;
using ComicsStore.Data.Repositories.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces;
using ComicsStore.Data.Repositories.Interfaces.CrossRepository;
using ComicsStore.Data.Repositories.Interfaces.MainRepository;
using ComicsStore.Data.Repositories.MainRepository;
using ComicsStore.Data.Repositories.ViewRepository;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Services;
using ComicsStore.MiddleWare.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComicsLibrary.Common;

public class ResolveDependencies
{
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("ComicsStore");
        _ = services.AddDbContext<ComicsStoreDbContext>(options => options
            .UseSqlite(conn, p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .EnableSensitiveDataLogging());
        //                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

        _ = services.AddScoped<IBasicListViewModel<ArtistOutputModel>, ArtistListViewModel>();
        _ = services.AddScoped<IBasicListViewModel<BookOutputModel>, BookListViewModel>();
        _ = services.AddScoped<IBasicListViewModel<CharacterOutputModel>, CharacterListViewModel>();
        _ = services.AddScoped<IBasicListViewModel<CodeOutputModel>, CodeListViewModel>();
        _ = services.AddScoped<IBasicListViewModel<PublisherOutputModel>, PublisherListViewModel>();
        _ = services.AddScoped<IBasicListViewModel<SeriesOutputModel>, SeriesListViewModel>();
        _ = services.AddScoped<IBasicListViewModel<StoryOutputModel>, StoryListViewModel>();

        _ = services.AddTransient<ArtistViewModel>();
        _ = services.AddTransient<BookViewModel>();
        _ = services.AddTransient<CharacterViewModel>();
        _ = services.AddTransient<CodeViewModel>();
        _ = services.AddTransient<PublisherViewModel>();
        _ = services.AddTransient<SeriesViewModel>();
        _ = services.AddTransient<StoryViewModel>();
        _ = services.AddTransient<InformationViewModel>();
        _ = services.AddTransient<ReportViewModel>();

        _ = services.AddScoped<IArtistsService, ArtistsService>();
        _ = services.AddScoped<IBooksService, BooksService>();
        _ = services.AddScoped<ICharactersService, CharactersService>();
        _ = services.AddScoped<ICodesService, CodesService>();
        _ = services.AddScoped<IPublishersService, PublishersService>();
        _ = services.AddScoped<ISeriesService, SeriesService>();
        _ = services.AddScoped<IStoriesService, StoriesService>();

        _ = services.AddScoped<IInformationService, InformationService>();
        _ = services.AddScoped<IViewService, ViewService>();

        _ = services.AddScoped<IComicsStoreMainRepository<Artist, BasicSearch>, ArtistsRepository>();
        _ = services.AddScoped<IComicsStoreMainRepository<Book, BasicSearch>, BooksRepository>();
        _ = services.AddScoped<IComicsStoreMainRepository<Character, BasicSearch>, CharactersRepository>();
        _ = services.AddScoped<IComicsStoreMainRepository<Code, BasicSearch>, CodesRepository>();
        _ = services.AddScoped<IComicsStoreMainRepository<Publisher, BasicSearch>, PublishersRepository>();
        _ = services.AddScoped<IComicsStoreMainRepository<Series, SeriesSearch>, SeriesRepository>();
        _ = services.AddScoped<IComicsStoreMainRepository<Story, StorySearch>, StoriesRepository>();
        _ = services.AddScoped<IViewRepository<ExportBook, ViewSearch>, StorySeriesViewRepository>();
        _ = services.AddScoped<IViewRepository<ExportBook, IdSearch>, InformationViewRepository>();

        _ = services.AddScoped<IComicsStoreCrossRepository<BookArtist, IBookArtist>, BookArtistsRepository>();
        _ = services.AddScoped<IComicsStoreCrossRepository<BookPublisher, IBookPublisher>, BookPublishersRepository>();
        _ = services.AddScoped<IComicsStoreCrossRepository<BookSeries, IBookSeries>, BookSeriesRepository>();
        _ = services.AddScoped<IComicsStoreCrossRepository<StoryArtist, IStoryArtist>, StoryArtistsRepository>();
        _ = services.AddScoped<IComicsStoreCrossRepository<StoryBook, IStoryBook>, StoryBooksRepository>();
        _ = services.AddScoped<IComicsStoreCrossRepository<StoryCharacter, IStoryCharacter>, StoryCharactersRepository>();
    }
}
