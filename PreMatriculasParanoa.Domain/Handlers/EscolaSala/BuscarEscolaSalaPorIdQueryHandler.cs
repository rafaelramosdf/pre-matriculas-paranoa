using AutoMapper;
using Microsoft.Extensions.Logging;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Contracts;

namespace PreMatriculasParanoa.Domain.Handlers.EscolaSala
{
    public interface IBuscarEscolaSalaPorIdQueryHandler : IByIdQueryHandler<EscolaViewModel>
    {
    }

    public class BuscarEscolaSalaPorIdQueryHandler : IBuscarEscolaSalaPorIdQueryHandler
    {
        private readonly ILogger<BuscarEscolaSalaPorIdQueryHandler> logger;
        private readonly IEscolaRepository repository;
        private readonly IMapper mapper;

        public BuscarEscolaSalaPorIdQueryHandler(
            IEscolaRepository repository,
            IMapper mapper,
            ILogger<BuscarEscolaSalaPorIdQueryHandler> logger)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        public EscolaViewModel Execute(int id)
        {
            logger.LogInformation($"Iniciando handler BuscarEscolaSalaPorIdQueryHandler");
            var result = repository.GetOne(m => m.IdEscola == id, i => i.Salas);
            return mapper.Map<EscolaViewModel>(result);
        }
    }
}
