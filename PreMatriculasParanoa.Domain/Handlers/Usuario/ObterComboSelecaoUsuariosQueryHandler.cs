using AutoMapper;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Handlers.Usuario;

public interface IObterComboSelecaoUsuariosQueryHandler : ISelectQueryHandler
{
}

public class ObterComboSelecaoUsuariosQueryHandler : IObterComboSelecaoUsuariosQueryHandler
{
    private readonly ILogger<ObterComboSelecaoUsuariosQueryHandler> Logger;
    private readonly IUsuarioRepository Repository;
    private readonly IMapper Mapper;

    public ObterComboSelecaoUsuariosQueryHandler(
        IUsuarioRepository repository,
        IMapper mapper,
        ILogger<ObterComboSelecaoUsuariosQueryHandler> logger)
    {
        Logger = logger;
        Repository = repository;
        Mapper = mapper;
    }

    public IEnumerable<SelectResult> Execute(string search, int limit, int selected)
    {
        Logger.LogInformation($"Iniciando handler ObterComboSelecaoUsuariosQueryHandler");

        limit = limit < 1 || limit > 50 ? 50 : limit;

        var selectList = new List<SelectResult>();

        var query = !string.IsNullOrEmpty(search)
            ? Repository.List(x => x.Nome.Contains(search) || x.Email.Contains(search))
            : Repository.List();

        var list = query
            .Where(x => x.Ativo)
            .OrderBy(o => o.Nome)
            .Take(limit)
            .Select(s => new
            {
                s.IdUsuario,
                s.Nome,
                s.Perfil
            }).ToList();

        if (list.Any())
        {
            foreach (var item in list)
            {
                selectList.Add(new SelectResult
                {
                    Id = item.IdUsuario,
                    Text = $"{item.Nome.ToUpper()} | Perfil: {item.Perfil}"
                });
            }
        }

        if (selected > 0 && !selectList.Any(a => a.Id == selected))
        {
            var entity = Repository.Get(selected);
            selectList.Add(new SelectResult
            {
                Id = entity.Id,
                Text = $"{entity.Nome.ToUpper()} | Perfil: {entity.Perfil}"
            });

            selectList = selectList.OrderBy(o => o.Text).ToList();
        }

        return selectList;
    }
}
