using AutoMapper;
using PreMatriculasParanoa.Domain.Models.Base;
using PreMatriculasParanoa.Domain.Repositories.Base;
using PreMatriculasParanoa.Domain.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using PreMatriculasParanoa.Domain.Resources;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Handlers.PlanejamentoAnoLetivo;

public interface IAtualizarEntradaSequencialPlanejamentoAnoLetivoCommandHandler : IEditCommandHandler<PlanejamentoMatriculaSequencialViewModel>
{
}

public class AtualizarEntradaSequencialPlanejamentoAnoLetivoCommandHandler : IAtualizarEntradaSequencialPlanejamentoAnoLetivoCommandHandler
{
    private readonly ILogger<AtualizarEntradaSequencialPlanejamentoAnoLetivoCommandHandler> logger;
    private readonly IPlanejamentoSerieAnoRepository planejamentoSerieAnoRepository;
    private readonly IPlanejamentoMatriculaSequencialRepository planejamentoMatriculaSequencialRepository;
    protected readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AtualizarEntradaSequencialPlanejamentoAnoLetivoCommandHandler(
        IPlanejamentoSerieAnoRepository planejamentoSerieAnoRepository,
        IPlanejamentoMatriculaSequencialRepository planejamentoMatriculaSequencialRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<AtualizarEntradaSequencialPlanejamentoAnoLetivoCommandHandler> logger)
    {
        this.logger = logger;
        this.planejamentoSerieAnoRepository = planejamentoSerieAnoRepository;
        this.planejamentoMatriculaSequencialRepository = planejamentoMatriculaSequencialRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public CommandResult Execute(PlanejamentoMatriculaSequencialViewModel matriculaSequencial)
    {
        logger.LogInformation($"Iniciando handler AtualizarEntradaSequencialPlanejamentoAnoLetivoCommandHandler");

        try
        {
            var planejamentoSeriesAnosEscolaDestino = planejamentoSerieAnoRepository
                    .GetOne(x => x.PlanejamentoAnoLetivo.IdEscola == matriculaSequencial.IdEscolaDestino
                    && x.PlanejamentoAnoLetivo.AnoLetivo == matriculaSequencial.AnoLetivo
                    && x.PrimeiraSerieAno);

            var totalMatriculasSequencial = planejamentoMatriculaSequencialRepository
                .GetQuery(x => x.IdEscolaDestino == matriculaSequencial.IdEscolaDestino && x.AnoLetivo == matriculaSequencial.AnoLetivo)
                .ToList()?.Sum(s => s.TotalMatriculas) ?? 0;

            if (planejamentoSeriesAnosEscolaDestino?.PrimeiraSerieAno == true)
            {
                planejamentoSeriesAnosEscolaDestino.EntradaSequencial = totalMatriculasSequencial;
                planejamentoSerieAnoRepository.Update(planejamentoSeriesAnosEscolaDestino);
                unitOfWork.Commit();
            }
            return new CommandResult(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return new CommandResult(StatusCodes.Status500InternalServerError, ErrorMessageResource.ErroExclusaoRegistro);
        }
    }
}
