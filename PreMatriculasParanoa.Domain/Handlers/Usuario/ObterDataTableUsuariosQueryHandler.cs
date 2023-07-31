using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Queries.Contracts;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Queries.Filters;

namespace PreMatriculasParanoa.Domain.Handlers.Usuario;

public interface IObterDataTableUsuariosQueryHandler : IDataTableQueryHandler<UsuarioViewModel, UsuarioFilter> 
{
}

public class ObterDataTableUsuariosQueryHandler : IObterDataTableUsuariosQueryHandler
{
    private readonly ILogger<ObterDataTableUsuariosQueryHandler> Logger;
    private readonly IUsuarioQuery Query;
    private readonly IUsuarioRepository Repository;
    private readonly IMapper Mapper;

    public ObterDataTableUsuariosQueryHandler(
        IUsuarioQuery query,
        IUsuarioRepository repository,
        IMapper mapper,
        ILogger<ObterDataTableUsuariosQueryHandler> logger)
    {
        Logger = logger;
        Query = query;
        Repository = repository;
        Mapper = mapper;
    }

    public DataTableModel<UsuarioViewModel> Execute(UsuarioFilter filtro)
    {
        Logger.LogInformation($"Iniciando handler ObterDataTableUsuariosQueryHandler");

        var dataTableModel = new DataTableModel<UsuarioViewModel>();

        IQueryable<Models.Entities.Usuario> queryList = Repository
            .GetQuery(Query.ObterPesquisa(filtro));

        dataTableModel.ExecuteQuerySortAndPage(queryList, filtro, Query, Mapper);

        return dataTableModel;
    }
}
