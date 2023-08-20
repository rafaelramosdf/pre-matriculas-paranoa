using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System;
using PreMatriculasParanoa.Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoMatriculaSequencial;

public interface IIncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler : IEditCommandHandler<PlanejamentoMatriculaSequencialAgrupadoViewModel>
{
}

public class IncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler : IIncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler
{
    private readonly ILogger<IncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler> logger;
    private readonly IPlanejamentoMatriculaSequencialRepository repository;
    protected readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public IncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler(
        IPlanejamentoMatriculaSequencialRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<IncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler> logger)
    {
        this.logger = logger;
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public CommandResult Execute(PlanejamentoMatriculaSequencialAgrupadoViewModel vm)
    {
        logger.LogInformation($"Iniciando handler IncluirOuAtualizarPlanejamentoMatriculaSequencialCommandHandler");

        try
        {
            var planejamentoMatriculasSequenciaisAgrupadasResult = new PlanejamentoMatriculaSequencialAgrupadoViewModel 
            {
                AnoLetivo = vm.AnoLetivo,
                EscolasOrigem = vm.EscolasOrigem,
                EscolasDestino = vm.EscolasDestino,
                PeriodoMatriculaSequencial = vm.PeriodoMatriculaSequencial,
                MatriculasSequenciais = new List<PlanejamentoMatriculaSequencialViewModel>()
            };

            unitOfWork.BeginTransaction();

            foreach (var matriculaSequencialViewModel in vm.MatriculasSequenciais)
            {
                var matriculaSequencial = mapper.Map<Models.Entities.PlanejamentoMatriculaSequencial>(matriculaSequencialViewModel);
                repository.Update(matriculaSequencial);
                unitOfWork.Commit();

                planejamentoMatriculasSequenciaisAgrupadasResult.MatriculasSequenciais.Add(mapper.Map<PlanejamentoMatriculaSequencialViewModel>(matriculaSequencial));
            }

            unitOfWork.CommitTransaction();
            return new CommandResult(StatusCodes.Status200OK, mapper.Map<PlanejamentoMatriculaSequencialAgrupadoViewModel>(planejamentoMatriculasSequenciaisAgrupadasResult));
        }
        catch (Exception ex)
        {
            return new CommandResult(ex, vm);
        }
    }
}
