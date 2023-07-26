using AutoMapper;
using Microsoft.Extensions.Logging;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Contracts;

namespace PreMatriculasParanoa.Domain.Handlers.Select
{
    public interface ISalaPorIdSelectQueryHandler : IByIdQueryHandler<SalaViewModel>
    {
    }

    public class SalaPorIdSelectQueryHandler : ISalaPorIdSelectQueryHandler
    {
        private readonly ILogger<SalaPorIdSelectQueryHandler> logger;
        private readonly ISalaRepository repository;
        private readonly IMapper mapper;

        public SalaPorIdSelectQueryHandler(
            ISalaRepository repository,
            IMapper mapper,
            ILogger<SalaPorIdSelectQueryHandler> logger)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        public SalaViewModel Execute(int id)
        {
            logger.LogInformation($"Iniciando handler SalaPorIdSelectQueryHandler");
            var result = repository.GetOne(m => m.IdSala == id);
            return mapper.Map<SalaViewModel>(result);
        }
    }
}
