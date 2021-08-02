using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoDIO.Controllers.V1;
using ApiCatalogoDIO.Repositories;
using ApiCatalogoDIO.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using ExemploApiCatalogoJogos.Middleware;

namespace ApiCatalogoDIO
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
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
            //services.AddRazorPages();

            // #region LifeCycle
            // services.AddSingleton<IExampleSingleton, ExampleLifeCycle>();
            // services.AddScoped<IExampleScoped, ExampleLifeCycle>();
            // services.AddTransient<IExampleTransient, ExampleLifeCycle>();
            // #endregion
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiCatalogo", Version = "v1" });

                //var basePath = AppDomain.CurrentDomain.BaseDirectory;
                //var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                //c.IncludeXmlComments(Path.Combine(basePath, fileName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCatalogo v1"));
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
