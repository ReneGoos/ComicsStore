using System;
using AutoMapper;
using ComicsStore.Data.Model;
using ComicsStore.MiddleWare;
using ComicsStore.MiddleWare.Models.Search;
using ComicsStore.MiddleWare.Repositories;
using ComicsStore.MiddleWare.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ComicsStore.API
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
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
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
            services.AddScoped<IExportMementoService, ExportMementoService>();

            services.AddScoped<IStoryArtistsService, StoryArtistsService>();
            services.AddScoped<IBookSeriesService, BookSeriesService>();

            services.AddScoped<IComicsStoreRepository<Artist, BasicSearchModel>, ArtistsRepository>();
            services.AddScoped<IComicsStoreRepository<Book, BasicSearchModel>, BooksRepository>();
            services.AddScoped<IComicsStoreRepository<Character, BasicSearchModel>, CharactersRepository>();
            services.AddScoped<IComicsStoreRepository<Code, BasicSearchModel>, CodesRepository>();
            services.AddScoped<IComicsStoreRepository<Publisher, BasicSearchModel>, PublishersRepository>();
            services.AddScoped<IComicsStoreRepository<Series, BasicSearchModel>, SeriesRepository>();
            services.AddScoped<IStoriesRepository, StoriesRepository>();
            services.AddScoped<IExportMementoRepository, ExportMementoRepository>();

            services.AddScoped<IComicsStoreCrossRepository<BookPublisher>, BookPublishersRepository>();
            services.AddScoped<IComicsStoreCrossRepository<BookSeries>, BookSeriesRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryArtist>, StoryArtistsRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryBook>, StoryBooksRepository>();
            services.AddScoped<IComicsStoreCrossRepository<StoryCharacter>, StoryCharactersRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                             new Info
                             {
                                 Title = "ComicsStoreAPI",
                                 Version = "v1"
                             });

                /* Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                               {
                                   {
                                       "Bearer", new string[]
                                                 {
                                                 }
                                   }
                               };

                c.AddSecurityDefinition("Bearer",
                                        new ApiKeyScheme
                                        {
                                            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                                            Name = "Authorization",
                                            In = "header",
                                            Type = "apiKey"
                                        });

                c.AddSecurityRequirement(security);
                */
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                using (var context = serviceProvider.GetService<ComicsStoreDbContext>())
                {
//                  context.Database.SetInitializer(new CreateDatabaseIfNotExists<ComicsStoreDbContext>());
//                  context.Database.Migrate();
                    context.Database.EnsureCreated();
                    context.Database.ExecuteSqlCommand("PRAGMA foreign_keys = ON");
                }
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComicsStoreAPI V1");

                c.DocumentTitle = "Title Documentation";
                c.DocExpansion(DocExpansion.None);
            });

            Mapper.Initialize(cfg => cfg.AddProfile<ComicsStoreProfile>());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
