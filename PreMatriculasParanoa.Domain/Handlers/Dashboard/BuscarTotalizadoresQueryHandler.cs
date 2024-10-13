using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Contracts;

namespace PreMatriculasParanoa.Domain.Handlers.Dashboard
{
    public interface IBuscarTotalizadoresQueryHandler : IQueryHandler<DashboardTotalizadoresViewModel, int>
    {
    }

    public class BuscarTotalizadoresQueryHandler : IBuscarTotalizadoresQueryHandler
    {
        private readonly ILogger<BuscarTotalizadoresQueryHandler> logger;
        private readonly IPlanejamentoSerieAnoRepository planejamentoSerieAnoRepository;
        private readonly ISalaRepository salaRepository;
        private readonly IMapper mapper;

        public BuscarTotalizadoresQueryHandler(
            IPlanejamentoSerieAnoRepository planejamentoSerieAnoRepository,
            ISalaRepository salaRepository,
            IMapper mapper,
            ILogger<BuscarTotalizadoresQueryHandler> logger)
        {
            this.logger = logger;
            this.planejamentoSerieAnoRepository = planejamentoSerieAnoRepository;
            this.salaRepository = salaRepository;
            this.mapper = mapper;
        }

        public DashboardTotalizadoresViewModel Execute(int anoLetivo)
        {
            logger.LogInformation($"Iniciando handler BuscarTotalizadoresQueryHandler");

            var dashboardTotalizadoresViewModel = new DashboardTotalizadoresViewModel();
            
            dashboardTotalizadoresViewModel.TotalGeralVagas = Convert.ToInt32(salaRepository.GetQuery().Select(s => s.CapacidadeFisicaPadrao).Sum());
            
            dashboardTotalizadoresViewModel.TotalVagasPreenchidas = planejamentoSerieAnoRepository.GetQuery(p => p.PlanejamentoAnoLetivo.AnoLetivo == anoLetivo)
            .Select(s => new 
            { 
                total = s.EntradaSequencial + 
                s.EntradaCentralMatricula + 
                s.EntradaAprovadosSerieAnoAnterior + 
                s.EntradaRemanejamento + 
                s.EntradaRetidosSerieAnoAtual
            }).Sum(s => s.total);

            dashboardTotalizadoresViewModel.TotalVagasLiberadas = planejamentoSerieAnoRepository.GetQuery(p => p.PlanejamentoAnoLetivo.AnoLetivo == anoLetivo)
            .Where(m => m.UltimaSerieAno)
            .Select(s => new 
            { 
                total = s.SaidaAprovadosAnoAtual
            }).Sum(s => s.total);

            return dashboardTotalizadoresViewModel;
        }
    }
}
