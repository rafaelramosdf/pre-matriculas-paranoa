using AutoMapper;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using PreMatriculasParanoa.Domain.Resources;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoAnoLetivo;

public interface IExcluirPlanejamentoAnoLetivoCommandHandler : IDeleteCommandHandler
{
}

public class ExcluirPlanejamentoAnoLetivoCommandHandler : IExcluirPlanejamentoAnoLetivoCommandHandler
{
    private readonly ILogger<ExcluirPlanejamentoAnoLetivoCommandHandler> logger;
    private readonly IPlanejamentoAnoLetivoRepository repository;
    protected readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ExcluirPlanejamentoAnoLetivoCommandHandler(
        IPlanejamentoAnoLetivoRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ExcluirPlanejamentoAnoLetivoCommandHandler> logger)
    {
        this.logger = logger;
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public CommandResult Execute(int id)
    {
        logger.LogInformation($"Iniciando handler ExcluirPlanejamentoAnoLetivoCommandHandler");

        try
        {
            var planejamentoAnoLetivo = repository.GetOne(id);
            repository.Remove(planejamentoAnoLetivo);
            unitOfWork.Commit();
            return new CommandResult(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new CommandResult(StatusCodes.Status500InternalServerError, ErrorMessageResource.ErroExclusaoRegistro);
        }
    }
}
