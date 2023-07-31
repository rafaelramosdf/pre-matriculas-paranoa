using AutoMapper;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using PreMatriculasParanoa.Domain.Resources;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial;

public interface IExcluirPlanejamentoMatriculaSequencialCommandHandler : IDeleteCommandHandler
{
}

public class ExcluirPlanejamentoMatriculaSequencialCommandHandler : IExcluirPlanejamentoMatriculaSequencialCommandHandler
{
    private readonly ILogger<ExcluirPlanejamentoMatriculaSequencialCommandHandler> logger;
    private readonly IPlanejamentoMatriculaSequencialRepository repository;
    protected readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ExcluirPlanejamentoMatriculaSequencialCommandHandler(
        IPlanejamentoMatriculaSequencialRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ExcluirPlanejamentoMatriculaSequencialCommandHandler> logger)
    {
        this.logger = logger;
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public CommandResult Execute(int id)
    {
        logger.LogInformation($"Iniciando handler ExcluirPlanejamentoMatriculaSequencialCommandHandler");

        try
        {
            var PlanejamentoMatriculaSequencial = repository.GetOne(id);
            repository.Remove(PlanejamentoMatriculaSequencial);
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
