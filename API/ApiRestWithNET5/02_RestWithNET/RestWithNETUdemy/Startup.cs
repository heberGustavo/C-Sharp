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
using Microsoft.OpenApi.Models;

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

            #region Connection DB

            var conection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(option => option.UseMySql(conection));

            #endregion

            #region Support to Json and XML

            services.AddMvc(option =>
            {
                option.RespectBrowserAcceptHeader = true;

                option.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                option.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Person and Books - Web API",
                    Description = "The project was developed using .NET 5.0 Web API and MySQL.",
                    Contact = new OpenApiContact
                    {
                        Name = "Heber Gustavo",
                        Url = new Uri("https://www.linkedin.com/in/heber-gustavo/")
                    }
                });
            });

            #endregion

            #region Migration

            MigrationDataBase(conection);

            #endregion

            #region API Versioning
            
            services.AddApiVersioning();
            
            #endregion

            #region Dependency Injection

            services.AddScoped<IPersonBusiness, PersonBusinessImplamentation>();
            services.AddScoped<IBookBusiness, BookBusinessImplamentation>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Person and Books - Web API");
            });

            #endregion

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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
