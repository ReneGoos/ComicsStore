using AutoMapper;
using ComicsEntry.Views;
using ComicsLibrary.Helpers;
using ComicsLibrary.Navigation;
using ComicsLibrary.ViewModels;
using ComicsStore.MiddleWare.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Windows;

namespace ComicsEntry;

public partial class App : Application
{
    private readonly IHost _host;

    public static IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        _host = Host.CreateDefaultBuilder()  // Use default settings
                                             //new HostBuilder()          // Initialize an empty HostBuilder
                .ConfigureAppConfiguration((context, builder) =>
                {
                    // Add other configuration files...
                    _ = builder.AddJsonFile("appsettings.local.json", optional: true);
                }).ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                })
                .ConfigureLogging(logging =>
                {
                    // Add other loggers...
                })
                .Build();

        ServiceProvider = _host.Services;
    }

    private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        ResolveDependencies.AddServices(services, configuration);

        var mappingConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ComicsLibraryProfile>();
        },
        NullLoggerFactory.Instance);

        var mapper = mappingConfig.CreateMapper();
        _ = services.AddSingleton(mapper);

        _ = services.AddScoped<INavigationService,NavigationService>(serviceProvider =>
        {
            var navigationService = new NavigationService(serviceProvider);

            navigationService.Configure(StoreWindows.Artist, typeof(ArtistPage));
            navigationService.Configure(StoreWindows.Book, typeof(BookPage));
            navigationService.Configure(StoreWindows.Character, typeof(CharacterPage));
            navigationService.Configure(StoreWindows.Code, typeof(CodePage));
            navigationService.Configure(StoreWindows.OriginStory, typeof(OriginStoryPage));
            navigationService.Configure(StoreWindows.PseudonymArtist, typeof(PseudonymArtistPage));
            navigationService.Configure(StoreWindows.Publisher, typeof(PublisherPage));
            navigationService.Configure(StoreWindows.Series, typeof(SeriesPage));
            navigationService.Configure(StoreWindows.Story, typeof(StoryPage));

            navigationService.Configure(StoreWindows.Informational, typeof(InformationalWindow), false);
            navigationService.Configure(StoreWindows.Information, typeof(InformationWindow), false);
            navigationService.Configure(StoreWindows.Report, typeof(ReportWindow), false);
            return navigationService;
        });


        // Register all ViewModels.
        _ = services.AddSingleton<ComicsViewModel>();

        // Register all the Windows of the applications.
        _ = services.AddSingleton<StartWindow>();
        _ = services.AddTransient<InformationalWindow>();
        _ = services.AddTransient<InformationWindow>();
        _ = services.AddTransient<ReportWindow>();
        _ = services.AddTransient<MainWindow>();
        _ = services.AddTransient<ArtistPage>();
        _ = services.AddTransient<BookPage>();
        _ = services.AddTransient<CharacterPage>();
        _ = services.AddTransient<CodePage>();
        _ = services.AddTransient<OriginStoryPage>();
        _ = services.AddTransient<PseudonymArtistPage>();
        _ = services.AddTransient<PublisherPage>();
        _ = services.AddTransient<SeriesPage>();
        _ = services.AddTransient<StoryPage>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var startupForm = _host.Services.GetRequiredService<StartWindow>();
        startupForm.Show();
        //this.StartupUri = new System.Uri("./Views/StartWindow.xaml", System.UriKind.Relative);

        //var navigationService = ServiceProvider.GetRequiredService<NavigationService>();
        //_ = await navigationService.ShowDialogAsync(StoreWindows.MainWindow);

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();

        base.OnExit(e);
    }
}
