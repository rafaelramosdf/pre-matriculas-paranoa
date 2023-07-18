using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using PreMatriculasParanoa.Domain.Models.Base;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoAnoLetivo;

public interface IIncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler : IEditCommandHandler<PlanejamentoAnoLetivoViewModel>
{
}

public class IncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler : IIncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler
{
    private readonly ILogger<IncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler> logger;
    private readonly IPlanejamentoAnoLetivoRepository planejamentoAnoLetivoRepository;
    private readonly IPlanejamentoSerieAnoRepository planejamentoSerieAnoRepository;
    private readonly IPlanejamentoTurmaRepository planejamentoTurmaRepository;
    protected readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public IncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler(
        IPlanejamentoAnoLetivoRepository planejamentoAnoLetivoRepository,
        IPlanejamentoSerieAnoRepository planejamentoSerieAnoRepository,
        IPlanejamentoTurmaRepository planejamentoTurmaRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<IncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler> logger)
    {
        this.logger = logger;
        this.planejamentoAnoLetivoRepository = planejamentoAnoLetivoRepository;
        this.planejamentoSerieAnoRepository = planejamentoSerieAnoRepository;
        this.planejamentoTurmaRepository = planejamentoTurmaRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public CommandResult Execute(PlanejamentoAnoLetivoViewModel vm)
    {
        logger.LogInformation($"Iniciando handler IncluirOuAtualizarPlanejamentoAnoLetivoCommandHandler");

        try
        {
            var novoRegistro = vm.IdPlanejamentoAnoLetivo < 1;
            var planejamentoAnoLetivo = mapper.Map<Models.Entities.PlanejamentoAnoLetivo>(vm);
            
            if(novoRegistro) 
            {
                planejamentoAnoLetivoRepository.Update(planejamentoAnoLetivo);
                unitOfWork.Commit();
                return new CommandResult(StatusCodes.Status201Created, mapper.Map<PlanejamentoAnoLetivoViewModel>(planejamentoAnoLetivo));
            }
            else
            {
                unitOfWork.BeginTransaction();

                // Inicia as alterações das Séries/Anos
                var idSeriesAnos = vm.SeriesAnos.Where(m => m.IdPlanejamentoSerieAno > 0).Select(s => s.IdPlanejamentoSerieAno);
                var seriesAnosExcluidas = planejamentoSerieAnoRepository.GetQuery(m => m.IdPlanejamentoAnoLetivo == vm.IdPlanejamentoAnoLetivo && !idSeriesAnos.Contains(m.IdPlanejamentoSerieAno)).ToList();
                var seriesAnosAlteradas = planejamentoAnoLetivo.SeriesAnos.Where(m => m.IdPlanejamentoSerieAno > 0).ToList();
                var seriesAnosNovas = planejamentoAnoLetivo.SeriesAnos.Where(m => m.IdPlanejamentoSerieAno < 1).ToList();

                if (seriesAnosExcluidas?.Any() == true) 
                {
                    planejamentoSerieAnoRepository.Remove(seriesAnosExcluidas);
                    unitOfWork.Commit();
                }

                if(seriesAnosNovas?.Any() == true) 
                {
                    planejamentoSerieAnoRepository.Add(seriesAnosNovas); 
                    unitOfWork.Commit();
                }

                planejamentoSerieAnoRepository.Attach(seriesAnosAlteradas);
                unitOfWork.Commit();

                // Inicia as alterações das Turmas
                var idTurmas = vm.SeriesAnos.SelectMany(m => m.Turmas).Where(m => m.IdPlanejamentoTurma > 0).Select(s => s.IdPlanejamentoTurma);
                var turmasExcluidas = planejamentoTurmaRepository.GetQuery(m => idSeriesAnos.Contains(m.IdPlanejamentoSerieAno) && !idTurmas.Contains(m.IdPlanejamentoTurma)).ToList();
                var turmasAlteradas = planejamentoAnoLetivo.SeriesAnos.SelectMany(m => m.Turmas).Where(m => m.IdPlanejamentoTurma > 0).ToList();
                var turmasNovas = planejamentoAnoLetivo.SeriesAnos.SelectMany(m => m.Turmas).Where(m => m.IdPlanejamentoTurma < 1).ToList();

                if (turmasExcluidas?.Any() == true)
                {
                    planejamentoTurmaRepository.Remove(turmasExcluidas);
                    unitOfWork.Commit();
                }

                if (turmasNovas?.Any() == true)
                {
                    planejamentoTurmaRepository.Add(turmasNovas);
                    unitOfWork.Commit();
                }

                planejamentoTurmaRepository.Attach(turmasAlteradas);
                unitOfWork.Commit();

                // Salva a entidade principal PlanejamentoAnoLetivo
                planejamentoAnoLetivoRepository.Attach(planejamentoAnoLetivo);
                unitOfWork.Commit();

                unitOfWork.CommitTransaction();
                return new CommandResult(StatusCodes.Status200OK, mapper.Map<PlanejamentoAnoLetivoViewModel>(planejamentoAnoLetivo));
            }
        }
        catch (Exception ex)
        {
            return new CommandResult(ex, vm);
        }
    }
}
