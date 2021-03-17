using AutoMapper;
using ComicsLibrary.Helpers;
using ComicsLibrary.Navigation;
using ComicsLibrary.ViewModels;
using ComicsLibrary.Views;
using ComicsStore.MiddleWare.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace ComicsLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly IHost host;

        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            host = Host.CreateDefaultBuilder()  // Use default settings
                                                //new HostBuilder()          // Initialize an empty HostBuilder
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        // Add other configuration files...
                        builder.AddJsonFile("appsettings.local.json", optional: true);
                    }).ConfigureServices((context, services) =>
                    {
                        ConfigureServices(context.Configuration, services);
                    })
                    .ConfigureLogging(logging =>
                    {
                        // Add other loggers...
                    })
                    .Build();

            ServiceProvider = host.Services;
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            ResolveDependencies.AddServices(services, configuration);

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ComicsLibraryProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Add NavigationService for the application.
            services.AddScoped<NavigationService>(serviceProvider =>
            {
                var navigationService = new NavigationService(serviceProvider);
                navigationService.Configure(Navigation.Windows.ArtistWindow, typeof(ArtistWindow));
                navigationService.Configure(Navigation.Windows.ArtistsList, typeof(ArtistsList));
                navigationService.Configure(Navigation.Windows.DetailWindow, typeof(Window1));
                navigationService.Configure(Navigation.Windows.StoryWindow, typeof(StoryWindow));

                return navigationService;
            });

            // Register all ViewModels.
            services.AddSingleton<ComicsViewModel>();
            services.AddSingleton<ArtistViewModel>();
            services.AddSingleton<StoryViewModel>();

            // Register all the Windows of the applications.
            services.AddTransient<ArtistWindow>();
            services.AddTransient<ArtistsList>();
            services.AddTransient<Window1>();
            services.AddTransient<StoryWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var navigationService = ServiceProvider.GetRequiredService<NavigationService>();
            //await navigationService.ShowAsync(Navigation.Windows.DetailWindow);
            await navigationService.ShowAsync(Navigation.Windows.StoryWindow);
            //await navigationService.ShowAsync(Navigation.Windows.ArtistsList);

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
