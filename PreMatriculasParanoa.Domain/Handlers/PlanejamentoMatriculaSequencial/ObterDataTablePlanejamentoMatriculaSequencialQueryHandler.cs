using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters;
using Microsoft.EntityFrameworkCore;
using PreMatriculasParanoa.Domain.Models.Entities;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial;

public interface IObterDataTablePlanejamentoMatriculaSequencialQueryHandler : IDataTableQueryHandler<PlanejamentoMatriculaSequencialAgrupadoViewModel, PlanejamentoMatriculaSequencialFilter> 
{
}

public class ObterDataTablePlanejamentoMatriculaSequencialQueryHandler : IObterDataTablePlanejamentoMatriculaSequencialQueryHandler
{
    private readonly ILogger<ObterDataTablePlanejamentoMatriculaSequencialQueryHandler> logger;
    private readonly IPlanejamentoMatriculaSequencialQuery query;
    private readonly IPlanejamentoMatriculaSequencialRepository repository;
    private readonly IMapper mapper;

    public ObterDataTablePlanejamentoMatriculaSequencialQueryHandler(
        IPlanejamentoMatriculaSequencialQuery query,
        IPlanejamentoMatriculaSequencialRepository repository,
        IMapper mapper,
        ILogger<ObterDataTablePlanejamentoMatriculaSequencialQueryHandler> logger)
    {
        this.logger = logger;
        this.query = query;
        this.repository = repository;
        this.mapper = mapper;
    }

    public DataTableModel<PlanejamentoMatriculaSequencialAgrupadoViewModel> Execute(PlanejamentoMatriculaSequencialFilter filtro)
    {
        logger.LogInformation($"Iniciando handler ObterDataTablePlanejamentoMatriculaSequencialQueryHandler");

        var dataTableModel = new DataTableModel<PlanejamentoMatriculaSequencialAgrupadoViewModel>();

        IQueryable<Models.Entities.PlanejamentoMatriculaSequencial> queryList = repository.GetQuery(query.ObterPesquisa(filtro))
            .Include(i => i.EscolaOrigem)
            .Include(i => i.EscolaDestino);

        queryList = dataTableModel.GetQuerySort(queryList, filtro, query);

        var planejamentoMatriculaSequencialAgrupados = queryList.GroupBy(g => g.IdEscolaOrigem).Select(s => 
        new PlanejamentoMatriculaSequencialAgrupado 
        {
            AnoLetivo = s.FirstOrDefault().AnoLetivo,
            EscolaOrigem = s.FirstOrDefault().EscolaOrigem,
            IdEscolaOrigem = s.FirstOrDefault().IdEscolaOrigem,
            SerieAnoOrigem = s.FirstOrDefault().SerieAnoOrigem,
            MatriculasSequenciais = s.Select(s => s)
        });

        planejamentoMatriculaSequencialAgrupados = dataTableModel.GetQueryPage(planejamentoMatriculaSequencialAgrupados, filtro);

        dataTableModel.ExecuteQuery(planejamentoMatriculaSequencialAgrupados, mapper);

        return dataTableModel;
    }
}
