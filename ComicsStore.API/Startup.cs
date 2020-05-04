using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using ComicsStore.MiddleWare.Repositories.Interfaces;
using ComicsStore.MiddleWare.Services;
using ComicsStore.MiddleWare.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ComicsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ComicsStoreClient/dist";
            });

            //from here copied
            //var connSqlServer = @"Server=(localdb)\MSSQLLocalDB;Database=ComicsStoreAPI;Trusted_Connection=True;MultipleActiveResultSets=true;AttachDBFileName=D:\SQLLite\ComicsStoreAPI.mdf";
            var conn = Configuration.GetConnectionString("ComicsStore");
            services.AddDbContext<ComicsStoreDbContext>(options => options.UseSqlite(conn));

            services.AddScoped<IArtistsService, ArtistsService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<ICharactersService, CharactersService>();
            services.AddScoped<ICodesService, CodesService>();
            services.AddScoped<IPublishersService, PublishersService>();
            services.AddScoped<ISeriesService, SeriesService>();
            services.AddScoped<IStoriesService, StoriesService>();
            services.AddScoped<IExportBooksService, ExportBooksService>();

            services.AddScoped<IStoryArtistsService, StoryArtistsService>();
            services.AddScoped<IBookSeriesService, BookSeriesService>();

            services.AddScoped<IComicsStoreRepository<Artist, BasicSearchModel>, ArtistsRepository>();
            services.AddScoped<IComicsStoreRepository<Book, BasicSearchModel>, BooksRepository>();
            services.AddScoped<IComicsStoreRepository<Character, BasicSearchModel>, CharactersRepository>();
            services.AddScoped<IComicsStoreRepository<Code, BasicSearchModel>, CodesRepository>();
            services.AddScoped<IComicsStoreRepository<Publisher, BasicSearchModel>, PublishersRepository>();
            services.AddScoped<IComicsStoreRepository<Series, BasicSearchModel>, SeriesRepository>();
            services.AddScoped<IStoriesRepository, StoriesRepository>();
            services.AddScoped<IExportBooksRepository, StorySeriesRepository>();

            services.AddScoped<IComicsStoreCrossRepository<BookPublisher>, BookPublishersRepository>();
            services.AddScoped<IComicsStoreCrossRepository<BookSeries>, BookSeriesRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryArtist>, StoryArtistsRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryBook>, StoryBooksRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryCharacter>, StoryCharactersRepository>();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ComicsStoreProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                             new OpenApiInfo
                             {
                                 Title = "ComicsStoreAPI",
                                 Version = "v1"
                             });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseCors(MyAllowSpecificOrigins);
            app.UseCors(builder =>
            {
                builder.WithOrigins("*");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComicsStoreAPI V1");

                c.DocumentTitle = "Title Documentation";
                c.DocExpansion(DocExpansion.None);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ComicsStoreClient";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
