using AutoMapper;
using Microsoft.Extensions.Logging;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Contracts;

namespace PreMatriculasParanoa.Domain.Handlers.Select
{
    public interface IEscolaPorIdSelectQueryHandler : IByIdQueryHandler<EscolaViewModel>
    {
    }

    public class EscolaPorIdSelectQueryHandler : IEscolaPorIdSelectQueryHandler
    {
        private readonly ILogger<EscolaPorIdSelectQueryHandler> logger;
        private readonly IEscolaRepository repository;
        private readonly IMapper mapper;

        public EscolaPorIdSelectQueryHandler(
            IEscolaRepository repository,
            IMapper mapper,
            ILogger<EscolaPorIdSelectQueryHandler> logger)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        public EscolaViewModel Execute(int id)
        {
            logger.LogInformation($"Iniciando handler EscolaPorIdSelectQueryHandler");
            var result = repository.GetOne(m => m.IdEscola == id);
            return mapper.Map<EscolaViewModel>(result);
        }
    }
}
