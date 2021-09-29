using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using TeusControleLite.Application.Interfaces.Repositories.BaseRepositories;
using TeusControleLite.Application.Interfaces.Repository;
using TeusControleLite.Application.Interfaces.Services;
using TeusControleLite.Application.Services;
using TeusControleLite.Domain.Dtos;
using TeusControleLite.Domain.Models;
using TeusControleLite.Infrastructure.Context;
using TeusControleLite.Infrastructure.Repository;

namespace TeusControleLite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();

            // Serviços
            services.AddScoped<IProductsService, ProductsService>();

            // Repositórios
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseDoubleRepository<>), typeof(BaseDoubleRepository<>));
            services.AddScoped<IProductsRepository, ProductsRepository>();

            // Mapeamento
            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<CreateProductsModel, Products>();
                config.CreateMap<UpdateProductsModel, Products>();
                config.CreateMap<Products, ProductsModel>();
            }).CreateMapper());

            // Configuração da base
            services.AddDbContext<ApiContext>(options => {
                options.UseMySql(
                    Configuration.GetConnectionString("MyConnection"),
                    new MySqlServerVersion(new Version(8, 0, 11))
                );
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Teus Controle Lite"
                    }
                );

                c.IncludeXmlComments(XmlCommentsFilePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}"
                );
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "TeusControleLite"
                );
            });
        }
    }
}
