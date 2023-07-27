using AutoMapper;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace PreMatriculasParanoa.Domain.Handlers.EscolaSala;

public interface IExcluirEscolaSalaCommandHandler : IDeleteCommandHandler
{
}

public class ExcluirEscolaSalaCommandHandler : IExcluirEscolaSalaCommandHandler
{
    private readonly ILogger<ExcluirEscolaSalaCommandHandler> logger;
    private readonly IEscolaRepository repository;
    private readonly ISalaRepository salaRepository;
    protected readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ExcluirEscolaSalaCommandHandler(
        IEscolaRepository repository,
        ISalaRepository salaRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ExcluirEscolaSalaCommandHandler> logger)
    {
        this.logger = logger;
        this.repository = repository;
        this.salaRepository = salaRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public CommandResult Execute(int id)
    {
        logger.LogInformation($"Iniciando handler ExcluirEscolaSalaCommandHandler");

        try
        {
            unitOfWork.BeginTransaction();

            var escola = repository.GetOne(id);
            var salas = salaRepository.GetQuery(m => m.IdEscola == escola.IdEscola).ToList();

            salaRepository.Remove(salas);
            unitOfWork.Commit();

            repository.Remove(escola);
            unitOfWork.Commit();

            unitOfWork.CommitTransaction();
            return new CommandResult(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return new CommandResult(ex);
        }
    }
}
