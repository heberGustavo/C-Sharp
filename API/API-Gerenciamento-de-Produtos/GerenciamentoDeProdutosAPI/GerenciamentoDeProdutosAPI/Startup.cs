using AutoMapper;
using GerenciamentoDeProdutosAPI.CrossCutting.DependencyGroups;
using GerenciamentoDeProdutosAPI.CrossCutting.MappingGroups;
using GerenciamentoDeProdutosAPI.Domain.IBusiness.Migration;
using GerenciamentoDeProdutosAPI.Migration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciamentoDeProdutosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IWebHostEnvironment Env { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToData>();
                cfg.AddProfile<DataToDomain>();
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            DataDependencyInjection.Register(services);
            DomainDependencyInjection.Register(services);

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Product Management",
                    Description = "The project was developed using .NET Web API, with the aim of carrying out Product Management",
                    Contact = new OpenApiContact
                    {
                        Name = "Heber Gustavo",
                        Url = new Uri("https://www.linkedin.com/in/heber-gustavo/")
                    }
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, [FromServices] IMigrationBusiness migrationBusiness)
        {
            //migrationBusiness.ExecutarAtualizacaoBancoDados();

            migrationBusiness.ExecutarAtualizacaoBancoDados();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerenciamento de Produtos API");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
