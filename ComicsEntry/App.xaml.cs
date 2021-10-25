using AutoMapper;
using ComicsLibrary.Helpers;
using ComicsLibrary.Navigation;
using ComicsLibrary.ViewModels;
using ComicsEntry.Views;
using ComicsStore.MiddleWare.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace ComicsEntry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
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
            });

            var mapper = mappingConfig.CreateMapper();
            _ = services.AddSingleton(mapper);

            // Add INavigationService for the application.
            _ = services.AddScoped(serviceProvider =>
            {
                var navigationService = new NavigationService(serviceProvider);
                navigationService.Configure(StoreWindows.ArtistWindow, typeof(ArtistWindow));
                navigationService.Configure(StoreWindows.BookWindow, typeof(BookWindow));
                navigationService.Configure(StoreWindows.CharacterWindow, typeof(CharacterWindow));
                navigationService.Configure(StoreWindows.CodeWindow, typeof(CodeWindow));
                navigationService.Configure(StoreWindows.PublisherWindow, typeof(PublisherWindow));
                navigationService.Configure(StoreWindows.SeriesWindow, typeof(SeriesWindow));
                navigationService.Configure(StoreWindows.StoryWindow, typeof(StoryWindow));
                navigationService.Configure(StoreWindows.StartWindow, typeof(StartWindow));
                navigationService.Configure(StoreWindows.ReportWindow, typeof(ReportWindow));

                return navigationService;
            });

            // Register all ViewModels.
            _ = services.AddSingleton<ComicsViewModel>();

            // Register all the Windows of the applications.
            _ = services.AddTransient<ArtistWindow>();
            _ = services.AddTransient<BookWindow>();
            _ = services.AddTransient<CharacterWindow>();
            _ = services.AddTransient<CodeWindow>();
            _ = services.AddTransient<PublisherWindow>();
            _ = services.AddTransient<SeriesWindow>();
            _ = services.AddTransient<StoryWindow>();
            _ = services.AddTransient<StartWindow>();
            _ = services.AddTransient<ReportWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var navigationService = ServiceProvider.GetRequiredService<NavigationService>();
            _ = await navigationService.ShowDialogAsync(StoreWindows.StartWindow);

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
