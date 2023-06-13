using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithNETUdemy.Model.Context;
using RestWithNETUdemy.Business;
using RestWithNETUdemy.Business.Implementations;
using System;
using System.Collections.Generic;
using RestWithNETUdemy.Repository;
using Serilog;
using RestWithNETUdemy.Repository.Base;
using Microsoft.Net.Http.Headers;
using RestWithNETUdemy.Hypermedia.Filters;
using RestWithNETUdemy.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace RestWithNETUdemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Conexão com o DB
            var conection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(option => option.UseMySql(conection));

            //Adicionando suporte para Json e XML
            services.AddMvc(option => 
            {
                option.RespectBrowserAcceptHeader = true;

                option.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                option.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFiltersOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            
            services.AddSingleton(filterOptions);

            //Migration
            MigrationDataBase(conection);

            //Versionamento de API
            services.AddApiVersioning();

            services.AddSwaggerGen(s => 
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rest API's ASP .NET Core 5",
                    Version = "v1",
                    Description = "Rest API's ASP .NET Core 5 - Descrição",
                    Contact = new OpenApiContact
                    {
                        Name = "Heber Gustavo",
                        Url = new Uri("https://github.com/heberGustavo/")
                    }
                });
            });

            //Injeção de dependencia
            services.AddScoped<IPersonBusiness, PersonBusinessImplamentation>();
            services.AddScoped<IBookBusiness, BookBusinessImplamentation>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(s => 
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest API's ASP .NET Core 5");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }

        private void MigrationDataBase(string conection)
        {
            try
            {
                var evolveConection = new MySql.Data.MySqlClient.MySqlConnection(conection);
                var evolve = new Evolve.Evolve(evolveConection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migration", "db/dataset" },
                    IsEraseDisabled = true
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Falha no Migration do Banco de Dados", ex);
                throw;
            }
        }
    }
}
