using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using PreMatriculasParanoa.Domain.Queries.Filters;
using Microsoft.EntityFrameworkCore;
using PreMatriculasParanoa.Domain.Models.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial;

public interface IBuscarAgrupadoPlanejamentoMatriculaSequencialQueryHandler : IByFilterQueryHandlerAsync<PlanejamentoMatriculaSequencialAgrupadoViewModel, PlanejamentoMatriculaSequencialFilter>
{
}

public class BuscarAgrupadoPlanejamentoMatriculaSequencialQueryHandler : IBuscarAgrupadoPlanejamentoMatriculaSequencialQueryHandler
{
    private readonly ILogger<BuscarAgrupadoPlanejamentoMatriculaSequencialQueryHandler> logger;
    private readonly IPlanejamentoMatriculaSequencialQuery query;
    private readonly IPlanejamentoMatriculaSequencialRepository repository;
    private readonly IPlanejamentoSerieAnoRepository planejamentoSerieAnoRepository;
    private readonly IEscolaRepository escolaRepository;
    private readonly IEscolaQuery escolaQuery;
    private readonly IMapper mapper;

    public BuscarAgrupadoPlanejamentoMatriculaSequencialQueryHandler(
        IPlanejamentoMatriculaSequencialQuery query,
        IPlanejamentoMatriculaSequencialRepository repository,
        IPlanejamentoSerieAnoRepository planejamentoSerieAnoRepository,
        IEscolaRepository escolaRepository,
        IEscolaQuery escolaQuery,
        IMapper mapper,
        ILogger<BuscarAgrupadoPlanejamentoMatriculaSequencialQueryHandler> logger)
    {
        this.logger = logger;
        this.query = query;
        this.repository = repository;
        this.planejamentoSerieAnoRepository = planejamentoSerieAnoRepository;
        this.escolaRepository = escolaRepository;
        this.escolaQuery = escolaQuery;
        this.mapper = mapper;
    }

    public async Task<PlanejamentoMatriculaSequencialAgrupadoViewModel> Execute(PlanejamentoMatriculaSequencialFilter filtro)
    {
        logger.LogInformation($"Iniciando handler BuscarAgrupadoPlanejamentoMatriculaSequencialQueryHandler");

        var planejamentoMatriculaSequencialAgrupadoViewModel = new PlanejamentoMatriculaSequencialAgrupadoViewModel();
        planejamentoMatriculaSequencialAgrupadoViewModel.MatriculasSequenciais = new List<PlanejamentoMatriculaSequencialViewModel>();
        planejamentoMatriculaSequencialAgrupadoViewModel.EscolasOrigem = new List<EscolaViewModel>();
        planejamentoMatriculaSequencialAgrupadoViewModel.EscolasDestino = new List<EscolaViewModel>();
        planejamentoMatriculaSequencialAgrupadoViewModel.PeriodoMatriculaSequencial = filtro.PeriodoMatriculaSequencial;
        planejamentoMatriculaSequencialAgrupadoViewModel.AnoLetivo = filtro.Year;

        IQueryable<Models.Entities.PlanejamentoMatriculaSequencial> queryListSequencial =
            repository.GetQuery(query.ObterPesquisa(filtro)).Include(i => i.EscolaOrigem).Include(i => i.EscolaDestino);

        queryListSequencial = queryListSequencial.Where(query.ObterFiltroAnoLetivo(filtro.Year));
        queryListSequencial = queryListSequencial.Where(query.ObterFiltroMatriculaSequencial(filtro));
        queryListSequencial = queryListSequencial.Where(m => m.EscolaOrigem.Regiao == filtro.Regiao && m.EscolaDestino.Regiao == filtro.Regiao);

        planejamentoMatriculaSequencialAgrupadoViewModel.MatriculasSequenciais =
            mapper.Map<List<PlanejamentoMatriculaSequencialViewModel>>(queryListSequencial.ToList()
            ?? new List<Models.Entities.PlanejamentoMatriculaSequencial>());

        // Buscar escolas origem:
        IQueryable<Escola> queryListEscolasOrigem =
            escolaRepository.GetQuery(escolaQuery.ObterPesquisa(new EscolaFilter()))
            .Where(query.ObterFiltroEscolaSequencial(filtro.PeriodoMatriculaSequencial, true))
            .Where(e => e.Regiao == filtro.Regiao);

        planejamentoMatriculaSequencialAgrupadoViewModel.EscolasOrigem =
            mapper.Map<List<EscolaViewModel>>(queryListEscolasOrigem.ToList() ?? new List<Escola>());

        // Buscar escolas destino:
        IQueryable<Escola> queryListEscolasDestino =
            escolaRepository.GetQuery(escolaQuery.ObterPesquisa(new EscolaFilter()))
            .Where(query.ObterFiltroEscolaSequencial(filtro.PeriodoMatriculaSequencial, false))
            .Where(e => e.Regiao == filtro.Regiao);

        planejamentoMatriculaSequencialAgrupadoViewModel.EscolasDestino =
            mapper.Map<List<EscolaViewModel>>(queryListEscolasDestino.ToList() ?? new List<Escola>());

        // Monta as matrículas sequenciais novas, quando não existe
        foreach (var escolaOrigem in planejamentoMatriculaSequencialAgrupadoViewModel.EscolasOrigem)
        {
            foreach (var escolaDestino in planejamentoMatriculaSequencialAgrupadoViewModel.EscolasDestino)
            {
                if (planejamentoMatriculaSequencialAgrupadoViewModel.MatriculasSequenciais
                    .Any(m => m.IdEscolaOrigem == escolaOrigem.Id && m.IdEscolaDestino == escolaDestino.Id))
                {
                    continue;
                }
                else
                {
                    planejamentoMatriculaSequencialAgrupadoViewModel.MatriculasSequenciais.Add(new PlanejamentoMatriculaSequencialViewModel
                    {
                        AnoLetivo = planejamentoMatriculaSequencialAgrupadoViewModel.AnoLetivo,
                        IdEscolaDestino = escolaDestino.Id,
                        IdEscolaOrigem = escolaOrigem.Id,
                        PeriodoMatriculaSequencial = planejamentoMatriculaSequencialAgrupadoViewModel.PeriodoMatriculaSequencial,
                        TotalMatriculas = 0
                    });
                }
            }
        }

        planejamentoMatriculaSequencialAgrupadoViewModel.MatriculasSequenciais =
            AtualizarTotalVagasDisponiveis(planejamentoMatriculaSequencialAgrupadoViewModel.MatriculasSequenciais);

        return planejamentoMatriculaSequencialAgrupadoViewModel;
    }

    private List<PlanejamentoMatriculaSequencialViewModel> AtualizarTotalVagasDisponiveis(List<PlanejamentoMatriculaSequencialViewModel> matriculasSequenciais)
    {
        foreach (var s in matriculasSequenciais)
        {
            var planejamentoSerieAnoDestino = mapper.Map<PlanejamentoSerieAnoViewModel>(planejamentoSerieAnoRepository
                .GetOne(m => m.PlanejamentoAnoLetivo.IdEscola == s.IdEscolaDestino
                && m.PlanejamentoAnoLetivo.AnoLetivo == s.AnoLetivo
                && m.PrimeiraSerieAno, i => i.Turmas) ?? new PlanejamentoSerieAno());

            var vagas = planejamentoSerieAnoDestino.TotalCapacidadeFisicaAcordada
                - (planejamentoSerieAnoDestino.EntradaCentralMatricula
                + planejamentoSerieAnoDestino.EntradaRemanejamento
                + planejamentoSerieAnoDestino.EntradaRetidosSerieAnoAtual);

            s.TotalVagasDisponiveis = vagas;
        }

        return matriculasSequenciais;
    }
}
