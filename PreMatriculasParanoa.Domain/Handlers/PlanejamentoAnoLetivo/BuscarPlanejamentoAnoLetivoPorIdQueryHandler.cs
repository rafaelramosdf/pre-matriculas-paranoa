using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoAnoLetivo
{
    public interface IBuscarPlanejamentoAnoLetivoPorIdQueryHandler : IByIdQueryHandler<PlanejamentoAnoLetivoViewModel>
    {
    }

    public class BuscarPlanejamentoAnoLetivoPorIdQueryHandler : IBuscarPlanejamentoAnoLetivoPorIdQueryHandler
    {
        private readonly ILogger<BuscarPlanejamentoAnoLetivoPorIdQueryHandler> logger;
        private readonly IPlanejamentoAnoLetivoRepository repository;
        private readonly IMapper mapper;

        public BuscarPlanejamentoAnoLetivoPorIdQueryHandler(
            IPlanejamentoAnoLetivoRepository repository,
            IMapper mapper,
            ILogger<BuscarPlanejamentoAnoLetivoPorIdQueryHandler> logger)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        public PlanejamentoAnoLetivoViewModel Execute(int id)
        {
            logger.LogInformation($"Iniciando handler BuscarPlanejamentoAnoLetivoPorIdQueryHandler");
            var result = repository.GetQuery(m => m.IdPlanejamentoAnoLetivo == id).Include(i => i.SeriesAnos).ThenInclude(i => i.Turmas).FirstOrDefault();
            return mapper.Map<PlanejamentoAnoLetivoViewModel>(result);
        }
    }
}
