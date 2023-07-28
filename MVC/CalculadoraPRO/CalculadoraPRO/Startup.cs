using AutoMapper;
using CalculadoraPRO.CrossCutting.DependencyGroups;
using CalculadoraPRO.CrossCutting.MappingGroups;
using CalculadoraPRO.Domain.IBusiness.Migration;
using CalculadoraPRO.Portal.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;

namespace CalculadoraPRO
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
            #region Cookie Policy

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
            });

            #endregion

            #region Mapper Configuration

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToData>();
                cfg.AddProfile<DataToDomain>();
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region Dependency Injection

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            DataDependencyInjection.Register(services);
            DomainDependencyInjection.Register(services);

            #endregion

            #region Authentication

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
               {
                   options.Cookie.HttpOnly = true;
                   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                   options.Cookie.SameSite = SameSiteMode.None;
                   options.Cookie.Name = ".CalculadoraPRO.AuthCookie";
                   options.LoginPath = "/Login/Index";
                   options.LogoutPath = "/Logout";
                   options.Cookie.MaxAge = TimeSpan.FromDays(1);
               });

            #endregion

            PolicyKeys.ConfigurePolicies(services);

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddControllersWithViews(options => options.Filters.Add(typeof(ValidateModelState)));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
