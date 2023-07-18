using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters;
using Microsoft.EntityFrameworkCore;

namespace PreMatriculasParanoa.Domain.Handlers.EscolaSala;

public interface IObterDataTableEscolaSalaQueryHandler : IDataTableQueryHandler<EscolaViewModel, EscolaFilter> 
{
}

public class ObterDataTableEscolaSalaQueryHandler : IObterDataTableEscolaSalaQueryHandler
{
    private readonly ILogger<ObterDataTableEscolaSalaQueryHandler> logger;
    private readonly IEscolaQuery query;
    private readonly IEscolaRepository repository;
    private readonly IMapper mapper;

    public ObterDataTableEscolaSalaQueryHandler(
        IEscolaQuery query,
        IEscolaRepository repository,
        IMapper mapper,
        ILogger<ObterDataTableEscolaSalaQueryHandler> logger)
    {
        this.logger = logger;
        this.query = query;
        this.repository = repository;
        this.mapper = mapper;
    }

    public DataTableModel<EscolaViewModel> Execute(EscolaFilter filtro)
    {
        logger.LogInformation($"Iniciando handler ObterDataTableEscolaSalaQueryHandler");

        var dataTableModel = new DataTableModel<EscolaViewModel>();

        IQueryable<Models.Entities.Escola> queryList = repository.GetQuery(query.ObterPesquisa(filtro)).Include(i => i.Salas);

        dataTableModel.SortAndPage(queryList, filtro, query, mapper);

        return dataTableModel;
    }
}
