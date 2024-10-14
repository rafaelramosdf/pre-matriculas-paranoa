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

            var jaPossuiPlanejamentoSendoPreenchido = planejamentoAnoLetivoRepository.GetQuery(m => m.IdEscola == vm.IdEscola && m.AnoLetivo == vm.AnoLetivo).Any();
            if(jaPossuiPlanejamentoSendoPreenchido)
            {
                return new CommandResult(400, vm, "Já existe planejamento para o Ano e Escola selecionados");
            }
            
            if(novoRegistro) 
            {
                planejamentoAnoLetivoRepository.Add(planejamentoAnoLetivo);
                unitOfWork.Commit();
                return new CommandResult(StatusCodes.Status201Created, mapper.Map<PlanejamentoAnoLetivoViewModel>(planejamentoAnoLetivo));
            }
            else
            {
                unitOfWork.BeginTransaction();

                // Preparando alterações das Séries/Anos
                var seriesAnosNovas = planejamentoAnoLetivo.SeriesAnos.Where(m => m.IdPlanejamentoSerieAno < 1).ToList();
                var seriesAnosAlteradas = planejamentoAnoLetivo.SeriesAnos.Where(m => m.IdPlanejamentoSerieAno > 0).ToList();
                var idSeriesAnosAlteradas = seriesAnosAlteradas.Select(s => s.IdPlanejamentoSerieAno);
                var seriesAnosExcluidas = planejamentoSerieAnoRepository.GetQuery(m => m.IdPlanejamentoAnoLetivo == vm.IdPlanejamentoAnoLetivo && !idSeriesAnosAlteradas.Contains(m.IdPlanejamentoSerieAno)).ToList();
                var idSeriesAnosExcluidas = seriesAnosExcluidas.Select(s => s.IdPlanejamentoSerieAno);

                // Preparando alterações das Turmas
                var turmasNovas = planejamentoAnoLetivo.SeriesAnos.Where(m => m.IdPlanejamentoSerieAno > 0).SelectMany(m => m.Turmas).Where(m => m.IdPlanejamentoTurma < 1).ToList();
                var turmasAlteradas = seriesAnosAlteradas.SelectMany(m => m.Turmas).Where(m => m.IdPlanejamentoTurma > 0).ToList();
                var idTurmasAlteradas = turmasAlteradas.Select(s => s.IdPlanejamentoTurma);
                var turmasExcluidas = planejamentoTurmaRepository.GetQuery(m => 
                    (idSeriesAnosAlteradas.Contains(m.IdPlanejamentoSerieAno) || idSeriesAnosExcluidas.Contains(m.IdPlanejamentoSerieAno)) 
                    && !idTurmasAlteradas.Contains(m.IdPlanejamentoTurma)).ToList();

                // 1 - Adiciona Series/Anos
                foreach (var serieAnoNova in seriesAnosNovas)
                {
                    planejamentoSerieAnoRepository.Attach(serieAnoNova, Microsoft.EntityFrameworkCore.EntityState.Added);
                    unitOfWork.Commit();
                }
                // 2 - Altera Series/Anos
                foreach (var serieAnoAlterada in seriesAnosAlteradas)
                {
                    serieAnoAlterada.Turmas = null;
                    planejamentoSerieAnoRepository.Attach(serieAnoAlterada, Microsoft.EntityFrameworkCore.EntityState.Modified);
                    unitOfWork.Commit();
                }
                // 3 - Deleta Turmas
                foreach (var turmaExcluida in turmasExcluidas)
                {
                    planejamentoTurmaRepository.Attach(turmaExcluida, Microsoft.EntityFrameworkCore.EntityState.Deleted);
                    unitOfWork.Commit();
                }
                // 4 - Deleta Series/Anos
                foreach (var serieAnoExcluida in seriesAnosExcluidas)
                {
                    planejamentoSerieAnoRepository.Attach(serieAnoExcluida, Microsoft.EntityFrameworkCore.EntityState.Deleted);
                    unitOfWork.Commit();
                }
                // 5 - Adiciona Turmas
                foreach (var turmaNova in turmasNovas)
                {
                    planejamentoTurmaRepository.Attach(turmaNova, Microsoft.EntityFrameworkCore.EntityState.Added);
                    unitOfWork.Commit();
                }
                // 6 - Altera Turmas
                foreach (var turmaAlterada in turmasAlteradas)
                {
                    planejamentoTurmaRepository.Attach(turmaAlterada, Microsoft.EntityFrameworkCore.EntityState.Modified);
                    unitOfWork.Commit();
                }

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
