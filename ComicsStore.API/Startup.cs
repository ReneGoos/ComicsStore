using AutoMapper;
using ComicsStore.Data.Common;
using ComicsStore.MiddleWare;
using ComicsStore.MiddleWare.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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
            _ = services.AddControllersWithViews()
                        .AddNewtonsoftJson(options =>
                        {
                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                            options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                        }
                    );

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ComicsStoreClient/dist";
            });

            //from here copied
            //var connSqlServer = @"Server=(localdb)\MSSQLLocalDB;Database=ComicsStoreAPI;Trusted_Connection=True;MultipleActiveResultSets=true;AttachDBFileName=D:\SQLLite\ComicsStoreAPI.mdf";

            ResolveDependencies.AddServices(services, Configuration);

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ComicsStoreProfile>();
            });

            var mapper = mappingConfig.CreateMapper();
            _ = services.AddSingleton(mapper);

            _ = services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1",
                               new OpenApiInfo
                               {
                                   Title = "ComicsStoreAPI",
                                   Version = "v1"
                               });
              });

            _ = services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ComicsStoreDbContext context)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                //context.Database.EnsureCreated();
            }
            else
            {
                _ = app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                _ = app.UseHsts();
            }

            //app.UseCors(MyAllowSpecificOrigins);
            _ = app.UseCors(builder =>
              {
                  _ = builder.WithOrigins("*");
              });

            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComicsStoreAPI V1");

                  c.DocumentTitle = "ComicsStore API";
                  c.DocExpansion(DocExpansion.None);
              });

            _ = app.UseHttpsRedirection();
            _ = app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            _ = app.UseRouting();

            _ = app.UseEndpoints(endpoints =>
              {
                  _ = endpoints.MapControllerRoute(
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
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    //spa.UseAngularCliServer(npmScript: "start");
                    //spa.Options.StartupTimeout = TimeSpan.FromSeconds(200);
                }
            });
        }
    }
}
