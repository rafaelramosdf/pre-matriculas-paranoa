using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using PreMatriculasParanoa.Domain.Models.Base;
using System.Collections.Generic;
using PreMatriculasParanoa.Domain.Queries.Filters;

namespace PreMatriculasParanoa.Domain.Handlers.Select;

public interface ISalaSelectQueryHandler : ISelectQueryHandler<SalaFilter>
{
}

public class SalaSelectQueryHandler : ISalaSelectQueryHandler
{
    private readonly ILogger<SalaSelectQueryHandler> Logger;
    private readonly ISalaRepository Repository;

    public SalaSelectQueryHandler(
        ISalaRepository repository,
        ILogger<SalaSelectQueryHandler> logger)
    {
        Logger = logger;
        Repository = repository;
    }

    public IEnumerable<SelectResult> Execute(SalaFilter salaFilter)
    {
        var selected = salaFilter.IdSala ?? 0;
        var limit = salaFilter.Limit ?? 50;
        var search = salaFilter.Search;

        Logger.LogInformation($"Iniciando handler SalasSelectQueryHandler");

        limit = limit < 1 || limit > 50 ? 50 : limit;

        var selectList = new List<SelectResult>();

        var query = salaFilter.IdEscola.HasValue 
            ? Repository.GetQuery(s => s.IdEscola == salaFilter.IdEscola) 
            : Repository.GetQuery(all => true);

        if (!string.IsNullOrEmpty(search)) 
        {
            query = query.Where(s => s.Numero.ToString() == search);
        }

        var list = query
            .OrderBy(o => o.Numero)
            .Take(limit)
            .Select(s => new
            {
                s.IdSala,
                s.Numero
            }).ToList();

        if (list.Any())
        {
            foreach (var item in list)
            {
                selectList.Add(new SelectResult
                {
                    Id = item.IdSala,
                    Text = $"{item.Numero}"
                });
            }
        }

        if (selected > 0 && !selectList.Any(a => a.Id == selected))
        {
            var entity = Repository.GetOne(selected);
            selectList.Add(new SelectResult
            {
                Id = entity.Id,
                Text = $"{entity.Numero}"
            });

            selectList = selectList.OrderBy(o => o.Text).ToList();
        }

        return selectList;
    }
}
