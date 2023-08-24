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
                c.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            });

            builder.Services.AddRefitClient<IPlanejamentoAnoLetivoApiContract>(settings).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri($"{urlBase}/planejamentos-anos-letivos");
                c.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            });

            builder.Services.AddRefitClient<IPlanejamentoMatriculaSequencialApiContract>(settings).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri($"{urlBase}/planejamentos-matriculas-sequenciais");
                c.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            });

            builder.Services.AddRefitClient<IEscolaSalaApiContract>(settings).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri($"{urlBase}/escolas-salas");
                c.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            });

            builder.Services.AddRefitClient<ISelectApiContract>(settings).ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri($"{urlBase}/selects");
                c.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
            });
        }
    }
}
