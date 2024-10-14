using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Contracts;

namespace PreMatriculasParanoa.Domain.Handlers.Dashboard
{
    public interface IBuscarProgressoPreenchimentoEscolasQueryHandler : IQueryHandler<DashboardProgressoPreenchimentoViewModel, int>
    {
    }

    public class BuscarProgressoPreenchimentoEscolasQueryHandler : IBuscarProgressoPreenchimentoEscolasQueryHandler
    {
        private readonly ILogger<BuscarProgressoPreenchimentoEscolasQueryHandler> logger;
        private readonly IPlanejamentoAnoLetivoRepository planejamentoAnoLetivoRepository;
        private readonly IEscolaRepository escolaRepository;
        private readonly IMapper mapper;

        public BuscarProgressoPreenchimentoEscolasQueryHandler(
            IPlanejamentoAnoLetivoRepository planejamentoAnoLetivoRepository,
            IEscolaRepository escolaRepository,
            IMapper mapper,
            ILogger<BuscarProgressoPreenchimentoEscolasQueryHandler> logger)
        {
            this.logger = logger;
            this.planejamentoAnoLetivoRepository = planejamentoAnoLetivoRepository;
            this.escolaRepository = escolaRepository;
            this.mapper = mapper;
        }

        public DashboardProgressoPreenchimentoViewModel Execute(int anoLetivo)
        {
            logger.LogInformation($"Iniciando handler BuscarProgressoPreenchimentoEscolasQueryHandler");

            var dashboardProgressoPreenchimentoViewModel = new DashboardProgressoPreenchimentoViewModel();

            dashboardProgressoPreenchimentoViewModel.TotalEscolasCadastradas = escolaRepository.GetQuery(m => m.Ativo).Count();
            dashboardProgressoPreenchimentoViewModel.TotalEscolasPreenchendo = planejamentoAnoLetivoRepository.GetQuery(m => m.AnoLetivo == anoLetivo).GroupBy(g => g.IdEscola).Count();

            return dashboardProgressoPreenchimentoViewModel;
        }
    }
}
