using AdministrativoImperial.CrossCutting.DependencyGroups;
using AdministrativoImperial.CrossCutting.MappingGroups;
using AdministrativoImperial.Domain.IBusiness.Migration;
using AdministrativoImperial.Portal.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativoImperial
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

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.Strict;
				options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
			});

			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<DomainToData>();
				cfg.AddProfile<DataToDomain>();
			});

			IMapper mapper = configuration.CreateMapper();
			services.AddSingleton(mapper);

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			DataDependencyInjection.Register(services);
			DomainDependencyInjection.Register(services);

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			   .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
			   {
				   options.Cookie.HttpOnly = true;
				   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				   options.Cookie.SameSite = SameSiteMode.None;
				   options.Cookie.Name = ".AdministrativoImperial.AuthCookie";
				   options.LoginPath = "/Login/Index";
				   options.LogoutPath = "/Logout";
				   options.Cookie.MaxAge = TimeSpan.FromDays(1);
			   });

			PolicyKeys.ConfigurePolicies(services);

			services.AddRazorPages().AddRazorRuntimeCompilation();
			services.AddSession(option =>
			{
				option.IdleTimeout = TimeSpan.FromDays(1);
				option.Cookie.HttpOnly = true;
				option.Cookie.IsEssential = true;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, [FromServices] IMigrationBusiness migrationBusiness)
		{
			#region Definindo a cultura padrão: pt-BR
			var supportedCultures = new[] { new CultureInfo("pt-BR") };
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});
			#endregion

			migrationBusiness.ExecutarAtualizacaoBancoDados();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Login}/{action=Index}/{id?}");
			});
		}
	}
}
