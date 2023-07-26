using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using PreMatriculasParanoa.Domain.Models.Base;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Domain.Handlers.Select;

public interface IEscolaSelectQueryHandler : ISelectQueryHandler
{
}

public class EscolaSelectQueryHandler : IEscolaSelectQueryHandler
{
    private readonly ILogger<EscolaSelectQueryHandler> Logger;
    private readonly IEscolaRepository Repository;

    public EscolaSelectQueryHandler(
        IEscolaRepository repository,
        ILogger<EscolaSelectQueryHandler> logger)
    {
        Logger = logger;
        Repository = repository;
    }

    public IEnumerable<SelectResult> Execute(string search, int limit = 50, int selected = 0)
    {
        Logger.LogInformation($"Iniciando handler EscolasSelectQueryHandler");

        limit = limit < 1 || limit > 50 ? 50 : limit;

        var selectList = new List<SelectResult>();

        var query = !string.IsNullOrEmpty(search)
            ? Repository.GetQuery(x => x.Nome.Contains(search))
            : Repository.GetQuery(all => true);

        var list = query
            .OrderBy(o => o.Nome)
            .Take(limit)
            .Select(s => new
            {
                s.IdEscola,
                s.Nome
            }).ToList();

        if (list.Any())
        {
            foreach (var item in list)
            {
                selectList.Add(new SelectResult
                {
                    Id = item.IdEscola,
                    Text = $"{item.Nome}"
                });
            }
        }

        if (selected > 0 && !selectList.Any(a => a.Id == selected))
        {
            var entity = Repository.GetOne(selected);
            selectList.Add(new SelectResult
            {
                Id = entity.Id,
                Text = $"{entity.Nome}"
            });

            selectList = selectList.OrderBy(o => o.Text).ToList();
        }

        return selectList;
    }
}
