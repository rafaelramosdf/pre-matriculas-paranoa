using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using PreMatriculasParanoa.Domain.Queries.Filters;
using System.Threading.Tasks;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial;

public interface IBuscarTotalMatriculasSequencialPorPlanejamentoQueryHandler : IQueryHandlerAsync<int, PlanejamentoMatriculaSequencialFilter>
{
}

public class BuscarTotalMatriculasSequencialPorPlanejamentoQueryHandler : IBuscarTotalMatriculasSequencialPorPlanejamentoQueryHandler
{
    private readonly ILogger<BuscarTotalMatriculasSequencialPorPlanejamentoQueryHandler> logger;
    private readonly IPlanejamentoMatriculaSequencialRepository repository;

    public BuscarTotalMatriculasSequencialPorPlanejamentoQueryHandler(
        IPlanejamentoMatriculaSequencialRepository repository,
        ILogger<BuscarTotalMatriculasSequencialPorPlanejamentoQueryHandler> logger)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task<int> Execute(PlanejamentoMatriculaSequencialFilter filtro)
    {
        return await Task.Run(() => 
        {
            logger.LogInformation($"Iniciando handler BuscarTotalMatriculasSequencialPorPlanejamentoQueryHandler");

            var totalMatriculasSequencial = repository.GetQuery(x => x.IdEscolaDestino == filtro.IdEscolaDestino && x.AnoLetivo == filtro.AnoLetivo)
                    .ToList()?.Sum(s => s.TotalMatriculas) ?? 0;

            return totalMatriculasSequencial;
        });
    }
}
