using PreMatriculasParanoa.Api.Filters;
using PreMatriculasParanoa.Domain.Handlers.Usuario;
using PreMatriculasParanoa.Domain.Queries;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using PreMatriculasParanoa.Infra.Context;
using PreMatriculasParanoa.Infra.Repository;
using PreMatriculasParanoa.Infra.UoW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using PreMatriculasParanoa.Domain.Handlers.EscolaSala;
using System.Text.Json.Serialization;
using PreMatriculasParanoa.Domain.Handlers.PlanejamentoAnoLetivo;
using PreMatriculasParanoa.Domain.Handlers.Select;
using PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial;

namespace PreMatriculasParanoa.Api
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

            services.AddControllers(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }); 

            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("PrivateKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "API SGPM", 
                    Version = "v1",
                    Description = "API do sistema de gestao de pre matriculas",
                    Contact = new OpenApiContact 
                    {
                        Name = ": Rafael Ramos | rafaelramosdf@gmail.com",
                        Email = "rafaelramosdf@gmail.com"
                    }
                });

                c.EnableAnnotations();
                c.SchemaFilter<SwaggerEnumDescriptionFilter>();
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });

            services.AddAutoMapper(typeof(Startup));

            RegisterRepositories(services);
            RegisterQueries(services);
            RegisterHandlers(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PreMatriculasParanoa.Api v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHsts();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddDbContext<EntityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEscolaRepository, EscolaRepository>();
            services.AddScoped<ISalaRepository, SalaRepository>();
            services.AddScoped<IPlanejamentoAnoLetivoRepository, PlanejamentoAnoLetivoRepository>();
            services.AddScoped<IPlanejamentoSerieAnoRepository, PlanejamentoSerieAnoRepository>();
            services.AddScoped<IPlanejamentoTurmaRepository, PlanejamentoTurmaRepository>();
            services.AddScoped<IPlanejamentoMatriculaSequencialRepository, PlanejamentoMatriculaSequencialRepository>();
        }

        private void RegisterQueries(IServiceCollection services)
        {
            services.AddScoped<IUsuarioQuery, UsuarioQuery>();
            services.AddScoped<IEscolaQuery, EscolaQuery>();
            services.AddScoped<IPlanejamentoAnoLetivoQuery, PlanejamentoAnoLetivoQuery>();
            services.AddScoped<IPlanejamentoMatriculaSequencialQuery, PlanejamentoMatriculaSequencialQuery>();
        }

        private void RegisterHandlers(IServiceCollection services)
        {
            #region Usuario
            services.AddScoped<IAlterarUsuarioCommandHandler, AlterarUsuarioCommandHandler>();
            services.AddScoped<IBuscarUsuarioPorIdQueryHandler, BuscarUsuarioPorIdQueryHandler>();
            services.AddScoped<IObterDataTableUsuariosQueryHandler, ObterDataTableUsuariosQueryHandler>();
            services.AddScoped<IObterComboSelecaoUsuariosQueryHandler, ObterComboSelecaoUsuariosQueryHandler>();
            services.AddScoped<IExcluirUsuarioCommandHandler, ExcluirUsuarioCommandHandler>();
            services.AddScoped<IIncluirUsuarioCommandHandler, IncluirUsuarioCommandHandler>();
            services.AddScoped<IAutenticarUsuarioCommandHandler, AutenticarUsuarioCommandHandler>();
            #endregion

            #region Escola
            services.AddScoped<IIncluirOuAtualizarEscolaSalaCommandHandler, IncluirOuAtualizarEscolaSalaCommandHandler>();
            services.AddScoped<IBuscarEscolaSalaPorIdQueryHandler, BuscarEscolaSalaPorIdQueryHandler>();
            services.AddScoped<IObterDataTableEscolaSalaQueryHandler, ObterDataTableEscolaSalaQueryHandler>();
            services.AddScoped<IExcluirEscolaSalaCommandHandler, ExcluirEscolaSalaCommandHandler>();
            #endregion

            #region PlanejamentoAnoLetivo
            services.AddScoped<IIncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler, IncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler>();
            services.AddScoped<IBuscarPlanejamentoAnoLetivoPorIdQueryHandler, BuscarPlanejamentoAnoLetivoPorIdQueryHandler>();
            services.AddScoped<IObterDataTablePlanejamentoAnoLetivoQueryHandler, ObterDataTablePlanejamentoAnoLetivoQueryHandler>();
            services.AddScoped<IExcluirPlanejamentoAnoLetivoCommandHandler, ExcluirPlanejamentoAnoLetivoCommandHandler>();
            #endregion

            #region PlanejamentoMatriculaSequencial
            services.AddScoped<IIncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler, IncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler>();
            services.AddScoped<IBuscarPlanejamentoMatriculaSequencialPorIdQueryHandler, BuscarPlanejamentoMatriculaSequencialPorIdQueryHandler>();
            services.AddScoped<IObterDataTablePlanejamentoMatriculaSequencialQueryHandler, ObterDataTablePlanejamentoMatriculaSequencialQueryHandler>();
            services.AddScoped<IExcluirPlanejamentoMatriculaSequencialCommandHandler, ExcluirPlanejamentoMatriculaSequencialCommandHandler>();
            #endregion

            #region Select
            services.AddScoped<IEscolaSelectQueryHandler, EscolaSelectQueryHandler>();
            services.AddScoped<IEscolaPorIdSelectQueryHandler, EscolaPorIdSelectQueryHandler>();
            services.AddScoped<ISalaSelectQueryHandler, SalaSelectQueryHandler>();
            services.AddScoped<ISalaPorIdSelectQueryHandler, SalaPorIdSelectQueryHandler>();
            #endregion
        }
    }
}
