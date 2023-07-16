using PreMatriculasParanoa.Web.Admin.Services.ApiContracts;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace PreMatriculasParanoa.Web.Admin.Services
{
    public static class StartupApiContracts
    {
        public static void ConfigureRefitServices(WebAssemblyHostBuilder builder)
        {
            string urlBase = builder.Configuration["UrlBaseApi"];

            var settings = new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            };

            builder.Services.AddRefitClient<IUsuarioApiContract>(settings).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri($"{urlBase}/usuarios");
            });

            builder.Services.AddRefitClient<IPlanejamentoAnoLetivoApiContract>(settings).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri($"{urlBase}/planejamentos-anos-letivos");
            });

            builder.Services.AddRefitClient<IEscolaSalaApiContract>(settings).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri($"{urlBase}/escolas-salas");
            });
        }
    }
}
