using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial
{
    public interface IBuscarPlanejamentoMatriculaSequencialPorIdQueryHandler : IByIdQueryHandler<PlanejamentoMatriculaSequencialAgrupadoViewModel>
    {
    }

    public class BuscarPlanejamentoMatriculaSequencialPorIdQueryHandler : IBuscarPlanejamentoMatriculaSequencialPorIdQueryHandler
    {
        private readonly ILogger<BuscarPlanejamentoMatriculaSequencialPorIdQueryHandler> logger;
        private readonly IPlanejamentoMatriculaSequencialRepository repository;
        private readonly IMapper mapper;

        public BuscarPlanejamentoMatriculaSequencialPorIdQueryHandler(
            IPlanejamentoMatriculaSequencialRepository repository,
            IMapper mapper,
            ILogger<BuscarPlanejamentoMatriculaSequencialPorIdQueryHandler> logger)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        public PlanejamentoMatriculaSequencialAgrupadoViewModel Execute(int id)
        {
            logger.LogInformation($"Iniciando handler BuscarPlanejamentoMatriculaSequencialPorIdQueryHandler");

            var planejamentoMatriculaSequencial = repository.GetOne(x => x.IdPlanejamentoMatriculaSequencial == id, i => i.EscolaOrigem);
            var planejamentoMatriculaSequencialAgrupadoViewModel = new PlanejamentoMatriculaSequencialAgrupadoViewModel
            {
                AnoLetivo = planejamentoMatriculaSequencial.AnoLetivo,
                IdEscolaOrigem = planejamentoMatriculaSequencial.IdEscolaOrigem,
                EscolaOrigem = mapper.Map<EscolaViewModel>(planejamentoMatriculaSequencial.EscolaOrigem),
                SerieAnoOrigem = planejamentoMatriculaSequencial.SerieAnoOrigem,
            };

            var listaPlanejamentosMatriculasSequenciais = repository.GetQuery(m => m.IdEscolaOrigem == planejamentoMatriculaSequencial.IdEscolaOrigem)
                .Include(i => i.EscolaOrigem)
                .Include(i => i.EscolaDestino)
                .ToList();

            if (listaPlanejamentosMatriculasSequenciais != null && listaPlanejamentosMatriculasSequenciais.Any())
            {
                planejamentoMatriculaSequencialAgrupadoViewModel.MatriculasSequenciais = 
                    mapper.Map<List<PlanejamentoMatriculaSequencialViewModel>>(listaPlanejamentosMatriculasSequenciais);
            }

            return planejamentoMatriculaSequencialAgrupadoViewModel;
        }
    }
}
