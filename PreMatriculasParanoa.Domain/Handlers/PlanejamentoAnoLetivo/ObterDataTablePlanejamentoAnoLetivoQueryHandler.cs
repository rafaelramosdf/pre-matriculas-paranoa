using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters;
using Microsoft.EntityFrameworkCore;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoAnoLetivo;

public interface IObterDataTablePlanejamentoAnoLetivoQueryHandler : IDataTableQueryHandler<PlanejamentoAnoLetivoViewModel, PlanejamentoAnoLetivoFilter> 
{
}

public class ObterDataTablePlanejamentoAnoLetivoQueryHandler : IObterDataTablePlanejamentoAnoLetivoQueryHandler
{
    private readonly ILogger<ObterDataTablePlanejamentoAnoLetivoQueryHandler> logger;
    private readonly IPlanejamentoAnoLetivoQuery query;
    private readonly IPlanejamentoAnoLetivoRepository repository;
    private readonly IMapper mapper;

    public ObterDataTablePlanejamentoAnoLetivoQueryHandler(
        IPlanejamentoAnoLetivoQuery query,
        IPlanejamentoAnoLetivoRepository repository,
        IMapper mapper,
        ILogger<ObterDataTablePlanejamentoAnoLetivoQueryHandler> logger)
    {
        this.logger = logger;
        this.query = query;
        this.repository = repository;
        this.mapper = mapper;
    }

    public DataTableModel<PlanejamentoAnoLetivoViewModel> Execute(PlanejamentoAnoLetivoFilter filtro)
    {
        logger.LogInformation($"Iniciando handler ObterDataTablePlanejamentoAnoLetivoQueryHandler");

        var dataTableModel = new DataTableModel<PlanejamentoAnoLetivoViewModel>();

        IQueryable<Models.Entities.PlanejamentoAnoLetivo> queryList = repository.GetQuery(query.ObterPesquisa(filtro))
            .Include(i => i.Escola)
            .Include(i => i.SeriesAnos).ThenInclude(t => t.Turmas);

        dataTableModel.ExecuteQuerySortAndPage(queryList, filtro, query, mapper);

        return dataTableModel;
    }
}
