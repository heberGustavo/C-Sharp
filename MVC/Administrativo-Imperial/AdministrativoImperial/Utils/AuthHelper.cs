using AdministrativoImperial.Domain;
using AdministrativoImperial.Domain.Models.EntityDomain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdministrativoImperial.Portal.Utils
{
    public class AuthHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public static UsuarioDTO USUARIO_LOGADO()
        {
            try
            {
                var usuario = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == PolicyKeys.USUARIO_LOGADO)?.Value;

                return JsonConvert.DeserializeObject<UsuarioDTO>(usuario);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ClaimsPrincipal GerarClaimsUsuarioLogado(UsuarioDTO usuario)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())
            };

            if (usuario != null)
            {
                usuario.senha = "";
                claims.Add(new Claim(PolicyKeys.USUARIO_LOGADO, JsonConvert.SerializeObject(usuario)));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }

    public class PolicyKeys
    {
        public static void ConfigurePolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyKeys.USUARIO_LOGADO, policy => policy.RequireClaim(PolicyKeys.USUARIO_LOGADO));
            });
        }

        public const string USUARIO_LOGADO = "usuario_logado";
    }
}
