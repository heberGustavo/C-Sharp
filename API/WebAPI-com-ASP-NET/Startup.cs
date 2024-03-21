using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartSchool_WEBAPI.Data;
using System;

namespace SmartSchool_WEBAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Conexão com o banco de dados
			var connection = Configuration["ConexaoSqlite:SqliteConnectionString"];
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(connection)
            );
            services.AddControllers()
                    // Configuração para ignorar o LOP infinito
                    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Ingeção de dependencias 
            services.AddScoped<IRepository, Repository>();

			services.AddSwaggerGen();

			services.AddSwaggerGen();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "API SmartSchool",
					Description = "The project was developed using .NET Web API, Entity Framework Core and SQ Lite",
					Contact = new OpenApiContact
					{
						Name = "Heber Gustavo",
						Url = new Uri("https://www.linkedin.com/in/heber-gustavo/")
					}
				});
			});


		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            //Liberar acesso do CORS
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
